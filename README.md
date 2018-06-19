# Necs

## Purpose
The main purpose of this software is to store user-written notes in an encrypted way.
Based on EntityFramework, all the user content is encrypted using Rijndael's algorithm. The only way to decrypt data is to know the security passphrase used to encrypt. 

## Software 
Written in C# .NET

----------------------

## Login screen
On the login screen, two possibilities:
- Create a new archive (defined by its name and its password)
- Open an existing archive
![Login window screenshot](https://github.com/8byr0/necs/blob/master/screenshots/login_window.PNG?raw=true)


## Editor main view
This window is made of:
- A list of all the notes (left-side)
- A new note form (bottom-left)
- An editor (right)
![Editor window screenshot](https://github.com/8byr0/necs/blob/master/screenshots/editor_window.PNG?raw=true)

## Improvements
In the future:
- Allow a note archive to be synchronized with a cloud solution (nextcloud, drive, onedrive...)

- Enable export options (to json, csv, whatever...)

- Implement a real editor with formatting options to set text appearance

-------------------------

## Credits
Credits for this application go to 
- Hugues BUREAU (https://github.com/8byr0)
- Alexandre BROHAN (https://github.com/lexlexlexlexlex)

-------------------------

## LICENSE
Copyright (c) 2018 Alexandre BROHAN - Hugues BUREAU

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
