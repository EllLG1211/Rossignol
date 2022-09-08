# Mermaid-generated simple class diagram
You'll find in this directory a class diagram generated with the help of gitea:

```mermaid
classDiagram
class Session
class KeyStore
class Credential{
    Category
    WebsiteName
    Url
}
class LoginPasswordCredential{
    Login
    Password
}
class OtpCredential{
    OtpCode
    - _expirationTimer
}
Credential <|-- LoginPasswordCredential
Credential <|-- OtpCredential
KeyStore *-- Credential
```

This is the source code. You can modify it in a text editor, and regenerate an svg or png thanks to [mermaid.live](https://mermaid.live)