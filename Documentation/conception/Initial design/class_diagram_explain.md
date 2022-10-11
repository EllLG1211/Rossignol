# Diagram explanation

## Data diagram

The model is based on the principle that a single password can be shared among multiple users. *Entry* classes belong to a *User*. The Entries know their owner. The other *User*(s), which know the password, won't be able to edit or re-share it.

[MCD](./MCD/database_mcd.mermaid.md)

----

## Class diagram

### Entries
The *Entry* class represents each entry in the app. It contains a *unique identifier*, the *Password* we save, a *Label*, the *Url* of the website that the password belongs to, and a *Note* for additionnal information.

*Entry* is abstract because its inheriting classes *ProprietaryEntry* and *SharedEntry* will enable us to check if we possess said *Entry* or not, based on its type. An *Entry* cannot be shared or edited by anyone other than the owner.

*SharedEntry* is the immuable class for the entry someone share with us. It don't overload property setter to still immuable.

*ProprietaryEntry* is the class for entries we own.

### Users
The users also need a layer of abstraction, because we must differenciate between a *User* which can be edited, and a *Sharer* which only gives access to its mail.

*LocalUser* is the user without an account on the server, and only local. It contains only a password.

*MailedUser* and his children is the class for a user registered on the server, with a mail and a password. *SharerUser* is the user who share a password with us, so we can edit it, and the *ConnectedUser* is the user logged in the app.

### Manager
The manager will manage all the app model.

[Class diagram](./Class/v2.mermaid.md)