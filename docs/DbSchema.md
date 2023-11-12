```mermaid
classDiagram

class Entity {
    +Guid Id
    +DateTime CreatedOn
    +DateTime? LastUpdatedOn
    +DateTime? DeletedOn
}

User --|> Entity

class User {
    +String FirstName
    +String LastName
    +String? MiddleName
    +String Email
    +Auth
    +DateTime LastActivity
}

User --o Wallet
Wallet --|> Entity

class Wallet {
    +Guid UserId
    +String Name
}

Wallet --o Transaction
Transaction --|> Entity

class Transaction {
    +Guid WalletId
    +Guid UserId
    +Money Amount
    +Guid TypeId
    +DateTime Timestamp
}

Transaction ..> Type
Type --|> Entity

class Type {
    +Guid Name
}
```