# PasswordManager

## Project Overview


Das Projekt **PasswordManager** bietet eine sichere und unabhängige Lösung zur Verwaltung von Passwörtern.  
In der heutigen digitalen Welt benötigen Nutzer eine wachsende Anzahl an Zugangsdaten für Apps, Webseiten und alltägliche Aufgaben.  
Diese Lösung begegnet Sicherheitsrisiken und Problemen in der Benutzerfreundlichkeit, die durch unsichere Speicherung, Passwortüberlastung oder Abhängigkeit von Cloud-Diensten entstehen.

### Key Features

- Speicherung von Passwörtern in verschlüsselter Form
- AES-256-Verschlüsselung für gespeicherte Daten
- TLS-Verschlüsselung bei der Datenübertragung
- Verwendung eines Master-Passworts
- Unterstützung für einmalige Wiederherstellungscodes
- AutoFill-Funktion für Passworteingaben
- Kategorisierung von Einträgen über Tags
- Gemeinsame Entwicklung als Desktopanwendung und Webanwendung


---

## Entity Overview

### Definition von ***User***

| Name         | Type    | MaxLength | Nullable | Unique | Db-Field | Access |
|--------------|---------|-----------|----------|--------|----------|--------|
| Nickname     | string  | 64        | Yes      | No     | Yes      | RW     |
| Age          | int     | —         | No       | No     | Yes      | RW     |
| Identity     | string  | 100       | No       | Yes    | Yes      | RW     |
| PasswordHash | string  | 256       | No       | No     | Yes      | RW     |
| PasswordSalt | string  | 256       | No       | No     | Yes      | RW     |
| Iterations   | int     | —         | No       | No     | Yes      | RW     |
| KeyAlgorithm | string  | 128       | No       | No     | Yes      | RW     |
| PublicKey    | byte[]  | —         | No       | No     | Yes      | RW     |

### Geschäftsregeln für `User`

| Rule | Subject | Type | Operation     | Description                                                                                       | Note                  |
|------|---------|------|---------------|---------------------------------------------------------------------------------------------------|-----------------------|
| U1   | `User`  | WENN | Create/Update | ein Benutzer angelegt oder bearbeitet wird,                                                       | Formatprüfung         |
|      |         | DANN |               | muss `Identity` eine gültige E-Mail-Adresse enthalten.                                            |                       |
| U2   | `User`  | WENN | Create        | ein Benutzer angelegt wird,                                                                       | Pflichtfeld           |
|      |         | DANN |               | darf `Identity` nicht leer sein.                                                                  |                       |
|!! U3 !!    | `User`  | WENN | Create        | ein Benutzer ein Masterpasswort festlegt,                                                         | Passwort-Komplexität  |
|      |         | DANN |               | muss dieses mindestens 8 Zeichen lang sein.   
| U4   | `User`  | WENN | Create/Update | ein `Nickname` angegeben wird,                                                                   | Eingabekonsistenz     |
|      |         | DANN |               | muss dieser mindestens 3 und maximal 64 Zeichen enthalten (ohne nur Leerzeichen).                | Optionales Feld       |
| U5   | `User`  | WENN | Create/Update | ein `Age` gesetzt wird,                                                                           | Altersfreigabe        |
|      |         | DANN |               | muss dieser mindestens 12 Jahre betragen.                                                        |                       |
| U6   | `User`  | WENN | Create/Update | ein `PublicKey` angegeben wird,                                                                   | Sicherheit             |
|      |         | DANN |               | darf dieser nicht leer sein.                                                                     |                       |        

---

### Definition von ***Vault***

| Name    | Type        | MaxLength | Nullable | Unique | Db-Field | Access |
|---------|-------------|-----------|----------|--------|----------|--------|
| Owner   | User        | —         | No       | No     | Yes      | RW     |
| Name    | string      | 100       | No       | No     | Yes      | RW     |
| Entries | VaultEntry[]| —         | Yes      | No     | Yes      | RW     |

---

### Definition von ***VaultEntry***

| Name          | Type    | MaxLength | Nullable | Unique | Db-Field | Access |
|---------------|---------|-----------|----------|--------|----------|--------|
| Name          | string  | —         | No       | No     | Yes      | RW     |
| Description   | string  | —         | No       | No     | Yes      | RW     |
| Url           | string  | —         | No       | No     | Yes      | RW     |
| EncryptedData | byte[]  | —         | No       | No     | Yes      | RW     |
| Nonce         | byte[]  | —         | No       | No     | Yes      | RW     |
| Tag           | byte[]  | —         | No       | No     | Yes      | RW     |
| Type          | Enum    | —         | No       | No     | Yes      | RW     |

---

### Definition von ***SharedVaultAccess***

| Name                | Type     | MaxLength | Nullable | Unique | Db-Field | Access |
|---------------------|----------|-----------|----------|--------|----------|--------|
| Vault               | Vault    | —         | No       | No     | Yes      | RW     |
| Owner               | User     | —         | No       | No     | Yes      | RW     |
| Recipient           | User     | —         | No       | No     | Yes      | RW     |
| EncryptedSharingKey | byte[]   | —         | No       | No     | Yes      | RW     |
| Permissions         | string   | —         | No       | No     | Yes      | RW     |
| ExpiresAt           | DateTime | —         | Yes      | No     | Yes      | RW     |

---