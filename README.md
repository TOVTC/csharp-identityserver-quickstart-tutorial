# IdentityServer - OpenID Connect
* IdentityServer is an OpenID Connect provider and implements OpenID Connect and OAuth 2.0 protocols
    * In short, a piece of software that issues security tokens to clients
* IdentityServer can: protect your resources, authenticate users, provide session sign-on and SSO, manage and authenticate clients, and validate tokens

## Terminology
* User - a human using a registered client to access resources
* Client - a piece of software tata requests tokens from IdentityServer that must be registered with the IdentityServer
    * Requests an identity toekn for authenticating a user
    * Requests an access token for accessing a resource
* Resources - anything that is to be protected with IdentityServer (identity data or "claims" and API's)
* Identity token - the outcome of the authentication process and contains the identifier for a user ("sub" or "subject claim"), information about how and when the user authenticated, and additional identity data
* Access token - allows access to an API resource, requested by the client, are forwarded to the API, and contain information about the client and the user in order to authorize access to data