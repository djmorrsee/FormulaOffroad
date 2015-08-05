Formula Offroad Database Schema
===============================

Users
-----
A 'user' is an account with a linked username and password.

Users should be allowed to 'log in' from any device.

In practice, username and password will be stored as playerprefs on device.
Each time a ghost or score is posted, username and password will be sent as well, and the server will perform authentication.

NOT maintaining a constant connection.

System will reject any post from a non-registered username password combo.

DO encryption!

- Creation
  - Username
  - Password

- Stored
  - Unique ID
  - Username
  - Password

Ghost Posting
-------------

- Required Data
  - Ghost Bytestring
  - Username
  - Password
  - Level ID (reference)

- Stored Data
  - Ghost Bytestring
  - User ID (reference)

Ghost Getting
-------------
- Required Data
  - Level ID (reference)

Level Storage
-------------
- Required Data
  - Name
  - ID
