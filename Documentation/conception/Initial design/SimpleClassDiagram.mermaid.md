# Simple class diagram
This simple class diagram is here to give a quick idea of our business classes. This is the source code. You can modify it in a text editor, and regenerate an svg or png thanks to [mermaid.live](https://mermaid.live).

```mermaid
classDiagram

class Session

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
}
Credential <|-- LoginPasswordCredential
Credential <|-- OtpCredential

class KeyStore
KeyStore *-- Credential : Credentials

class Authenticator
class EncryptionManager{
    string Encrypt(string)
    string Decrypt(string)
}
class CredentialAdder
CredentialAdder ..> Credential

class EntryFilter{
    <<abstract>>
    Credential* Filter(Credential*)
}
EntryFilter..> Credential

class FilterByTags
EntryFilter <|-- FilterByTags

class SettingsManager
SettingsManager --> SettingsManager : Instance$

class InputSanitizer{
    string Sanitize(string)
}
```