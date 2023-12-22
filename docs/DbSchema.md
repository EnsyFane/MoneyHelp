```mermaid
classDiagram

class Entity {
    +Guid Id
    +DateTime CreatedOn
    +DateTime? LastUpdatedOn
    +DateTime? DeletedOn
}

UserEntity --|> Entity

class UserEntity {
    +Guid UserId
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
Wallet --|> UserEntity

class Wallet {
    +String Name
}

Wallet --o Transaction
Transaction --|> UserEntity

class Transaction {
    +Guid WalletId
    +Guid TypeId
    +Decimal Amount
    +String? Description
    +DateTime Timestamp
}

Transaction ..> Type
Type --|> UserEntity

class Type {
    +String Name
}
```
