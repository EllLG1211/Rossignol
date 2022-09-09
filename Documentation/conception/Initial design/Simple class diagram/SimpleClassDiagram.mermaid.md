# Simple class diagram
This simple class diagram is here to give a quick idea of our business classes. This is the source code. You can modify it in a text editor, and regenerate an svg or png thanks to [mermaid.live](https://mermaid.live).

```mermaid
classDiagram
class Session
class KeyStore
class Credential{
    <<abstract>>
    UID
    Name
    Tag
    Url
    Protected: bool
}
class LoginPasswordCredential{
    Login
    Password
}
class OtpCredential{
    OtpCode
    ExpirationTimer
}

class Authenticator
class EncryptionManager{
    Encrypt(str)
    Decrypt(str)
}
class CredentialAdder

class EntryFilter{
    <<abstract>>
    Filter(Credential*): Credential*
}
class FilterByTags
EntryFilter <|-- FilterByTags

Credential <|-- LoginPasswordCredential
Credential <|-- OtpCredential
KeyStore *-- Credential : Credentials
CredentialAdder ..> Credential
EntryFilter..> Credential
```

KeyStore is our applicato