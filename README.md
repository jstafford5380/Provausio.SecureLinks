# Provausio.SecureLinks
An API that provides a generic way to hide values or data by creating a non-enumerable ID that can only be accessed by the intended recipient client, much like a private key pair.

This offers a solution for when you want to provide semi-secure links to data without needing an auth system. Use accordingly.

# Creating a Link
#### Sample Request
``` sh

POST /links HTTP/1.1
Host: localhost:5001
Content-Type: application/json
User-Agent: PostmanRuntime/7.15.2
Accept: */*
Cache-Control: no-cache
Postman-Token: 7aeed24b-95c3-4994-8ddf-95ae777ac6be,fdc22900-dfb5-44c5-912d-9450a8a94d0c
Host: localhost:5001
Accept-Encoding: gzip, deflate
Content-Length: 92
Connection: keep-alive
cache-control: no-cache

{
	"secrets": ["foobar"],
	"data": "hello world",
	"isEncrypted": false,
	"ttlSeconds": 60
}

```

| Property | Description |
|----------| ----------- |
| secrets  | A list of secrets that will be used to create the hash on the backend that will be used to retrieve your data later. |
| data     | This is the data that you want to create a link for.|
| isEncrypted | Informational. This is use to denote whether or not the data you are securing is encrypted. |
| ttlSeconds | The life span, in seconds. This is used to set the time that this link should be active.|


#### Sample Response

``` json
{
    "linkId": "q77sPOimPHcLCHy",
    "expiresAt": "2019-08-03T22:05:46.9504561+00:00"
}
```

| Property  | Description |
| --------  | ----------- |
| linkId    | This is the ID of the link that was generated for your data.
| expiresAt | This is the DateTime when the link will be deleted.


# Retrieving your data

#### Sample Request

```
GET /links/{{YOUR HASH HERE}} HTTP/1.1
Host: localhost:5001
User-Agent: PostmanRuntime/7.15.2
Accept: */*
Cache-Control: no-cache
Postman-Token: f96c127f-3dbb-4012-a0af-4c689cc5da9c,73fcbca4-d537-426c-8948-b4cb89e6d68a
Host: localhost:5001
Accept-Encoding: gzip, deflate
Connection: keep-alive
cache-control: no-cache
```

> Notice {{ YOUR HASH HERE }}

the `/links/{hash}` endpoint requires you to generate a SHA1 hash salted with the linkId and a secret. The secret must be one of the secrets used to generate the link.

Hash them together, separating them with a pipe, e.g. `sha1(linkId|secret)` and encoding the hash as Hex

#### Example in Javascript
``` javascript
let salt = `${linkId}|foobar`;
let hash = CryptoJS.SHA1(salt).toString(CryptoJS.enc.Hex);
```

#### Sample Response

``` json
{
    "value": "hello world"
}
```

> The secrets that are sent up during your CreateLink request are meant to be kept private on whatever system will be retrieving the data.


## Example Scenario

Imagine you wanted to send a link to a customer to bring them to a page that is meant to be private, but you don't want that user to have to log in. The landing page will need enough information from the link to retrieve and display the data but that information cannot be predictable/enumerable or else someone might be able to simply throw random data at the page to see what they can find.

In this scenario, consider the following:

- System A wants to send the link to a user and wants to make sure that only System B can retrieve the data, so it sends the data and the system B secret to the secured link generator and the generator responds with a LinkID of `q77sPOimPHcLCHy`. 
- System A then sends the following link to the user `https://mysite.com/landingpage/q77sPOimPHcLCHy`
- When the user clicks the link, it'll take them to the landing page at that address (let's call it system B).
- System B will use the linkId `q77sPOimPHcLCHy` along with its own secret to generate the hash `5041dd7435a95a3fd2a1f97bfdf8fbe75db3eacd` which it uses to request the data from the link generator. If the hash is correct, then the data is returned.

### Other Use Cases
- Generic resource sharing (e.g. sharing documents, configurations, etc.)
- Paramter obfuscation (e.g. when you don't want to put things like user IDs in the link)

## Notes
 - Yes, system A needs to know the secrets of all the authorized systems in order to request the link, so maybe "secret" is a misleading thing to call it. It's basically an identifier that only the client and server should be aware of.
 - The uniqueness of the LinkID is 1 in about 93 trillion.
 - The uniquess of the client secret is arbitrary.
 - The uniquess of the hash is variable depending on the number of active links still in the database, hence the expiration behavior. But for argument sake, I believe that even if you had 1.71 x 10<sup>155</sup> links in the database, then the chances of a collision/guess would still be something like 1 in 10<sup>18</sup>
