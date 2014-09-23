/*
	Copyright 2014 Steve Konves

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
*/

using System;
using System.IO;
using System.Security;
using System.Text;

namespace Konves.Csv
{
	/// <summary>
	/// Implements a <see cref="System.IO.TextWriter"/> for writing CSV fields and records to a stream in a particular encoding.
	/// </summary>
	public sealed class CsvStreamWriter : StreamWriter
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using UTF-8 encoding and the default buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream)
			: this(stream, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using UTF-8 encoding and the default buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, CsvEscapeMode escapeMode)
			: this(stream, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using UTF-8 encoding and the default buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(stream)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified file on the specified path, using the default encoding and buffer size.
		/// </summary>
		/// <param name="path">The complete file path to write to. <paramref name="path"/> can be a file name.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path)
			: this(path, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified file on the specified path, using the default encoding and buffer size.
		/// </summary>
		/// <param name="path">The complete file path to write to. <paramref name="path"/> can be a file name.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, CsvEscapeMode escapeMode)
			: this(path, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified file on the specified path, using the default encoding and buffer size.
		/// </summary>
		/// <param name="path">The complete file path to write to. <paramref name="path"/> can be a file name.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(path)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using the specified encoding and the default buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, Encoding encoding)
			: this(stream, encoding, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using the specified encoding and the default buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="escapeMode">The escape mode.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, Encoding encoding, CsvEscapeMode escapeMode)
			: this(stream, encoding, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using the specified encoding and the default buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, Encoding encoding, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(stream, encoding)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the default encoding and buffer size. If
		/// the file exists, it can be either overwritten or appended to. If the file
		/// does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		///     and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		///     is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append)
			: this(path, append, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the default encoding and buffer size. If
		/// the file exists, it can be either overwritten or appended to. If the file
		/// does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, CsvEscapeMode escapeMode)
			: this(path, append, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the default encoding and buffer size. If
		/// the file exists, it can be either overwritten or appended to. If the file
		/// does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(path, append)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using the specified encoding and buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">Sets the buffer size.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize"/> is negative.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, Encoding encoding, int bufferSize)
			: this(stream, encoding, bufferSize, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using the specified encoding and buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">Sets the buffer size.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize"/> is negative.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, Encoding encoding, int bufferSize, CsvEscapeMode escapeMode)
			: this(stream, encoding, bufferSize, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class
		/// for the specified stream, using the specified encoding and buffer size.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">Sets the buffer size.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.ArgumentException">
		///   <paramref name="stream"/> is not writable.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize"/> is negative.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="stream"/> is null.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(Stream stream, Encoding encoding, int bufferSize, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(stream, encoding, bufferSize)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the specified encoding and default buffer
		/// size. If the file exists, it can be either overwritten or appended to. If
		/// the file does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, Encoding encoding)
			: this(path, append, encoding, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the specified encoding and default buffer
		/// size. If the file exists, it can be either overwritten or appended to. If
		/// the file does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, Encoding encoding, CsvEscapeMode escapeMode)
			: this(path, append, encoding, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the specified encoding and default buffer
		/// size. If the file exists, it can be either overwritten or appended to. If
		/// the file does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, Encoding encoding, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(path, append, encoding)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the specified encoding and default buffer
		/// size. If the file exists, it can be either overwritten or appended to. If
		/// the file does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">Sets the buffer size.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize"/> is negative.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, Encoding encoding, int bufferSize)
			: this(path, append, encoding, bufferSize, c_defaultEscapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the specified encoding and default buffer
		/// size. If the file exists, it can be either overwritten or appended to. If
		/// the file does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">Sets the buffer size.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize"/> is negative.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, Encoding encoding, int bufferSize, CsvEscapeMode escapeMode)
			: this(path, append, encoding, bufferSize, escapeMode, c_defaultDelimiter, c_defaultEscapeChar)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamWriter"/> class for the specified
		/// file on the specified <paramref name="path"/>, using the specified encoding and default buffer
		/// size. If the file exists, it can be either overwritten or appended to. If
		/// the file does not exist, this constructor creates a new file.
		/// </summary>
		/// <param name="path">The complete file path to write to.</param>
		/// <param name="append">Determines whether data is to be appended to the file. If the file exists
		/// and <paramref name="append"/> is <c>false</c>, the file is overwritten. If the file exists and <paramref name="append"/>
		/// is <c>true</c>, the data is appended to the file. Otherwise, a new file is created</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">Sets the buffer size.</param>
		/// <param name="escapeMode">The escape mode to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escapeChar">The character used to escape fields.</param>
		/// <exception cref="System.UnauthorizedAccessException">
		/// Access is denied.</exception>
		///   
		/// <exception cref="System.ArgumentException">
		///   <paramref name="path"/> is an empty string ("").
		/// -or- <paramref name="path"/> contains the name of a system device (com1, com2, and so on).</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		///   <paramref name="path"/> is null.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize"/> is negative.</exception>
		///   
		/// <exception cref="System.IO.DirectoryNotFoundException">
		/// The specified <paramref name="path"/> is invalid, such as being on an unmapped drive</exception>
		///   
		/// <exception cref="System.IO.PathTooLongException">
		/// The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum
		/// length. For example, on Windows-based platforms, paths must be less than
		/// 248 characters, and file names must be less than 260 characters.</exception>
		///   
		/// <exception cref="System.IO.IOException">
		///   <paramref name="path"/> includes an incorrect or invalid syntax for file name, directory name,
		/// or volume label syntax</exception>
		///   
		/// <exception cref="System.Security.SecurityException">
		/// The caller does not have the required permission.</exception>
		[SecuritySafeCritical]
		public CsvStreamWriter(string path, bool append, Encoding encoding, int bufferSize, CsvEscapeMode escapeMode, char delimiter, char escapeChar)
			: base(path, append, encoding, bufferSize)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escapeChar;
			m_escapeMode = escapeMode;
		}

		#endregion

		/// <summary>
		/// Gets the character used to delimit fields.
		/// </summary>
		public char Delimiter
		{
			get { return m_delimiter; }
		}

		/// <summary>
		/// Gets the character used to escape fields.
		/// </summary>
		public char EscapeCharacter
		{
			get { return m_escapeCharacter; }
		}

		/// <summary>
		/// Gets the mode used to determine when to escape CSV fields.
		/// </summary>
		public CsvEscapeMode EscapeMode
		{
			get { return m_escapeMode; }
		}

		/// <summary>
		/// Writes an empty field to the text stream.
		/// </summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField()
		{
			WriteField(new char[] { }, 0, 0);
		}

		/// <summary>
		/// Writes a field containing the text representation of a Boolean value to the text stream.
		/// </summary>
		/// <param name="value">The Boolean to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(bool value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the character to the stream.
		/// </summary>
		/// <param name="value">The character to write.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		///   
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush"/> is true or the <see cref="T:System.IO.StreamWriter"/> buffer is full, and current writer is closed. </exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush"/> is true or the <see cref="T:System.IO.StreamWriter"/> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter"/> is at the end the stream. </exception>
		public void WriteField(char value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of a character array to the text stream.
		/// </summary>
		/// <param name="buffer">The character array to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs. </exception>
		public void WriteField(char[] buffer)
		{
			if (buffer == null)
				WriteField();
			else
				WriteField(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// Writes a field containing the text representation of a subarray of characters to the text stream.
		/// </summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">Starting index in the buffer.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="System.ArgumentException">
		/// The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>.</exception>
		///   
		/// <exception cref="System.ArgumentNullException">
		/// The <paramref name="buffer"/> parameter is null.</exception>
		///   
		/// <exception cref="System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> or <paramref name="count"/> is negative.</exception>
		///   
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(char[] buffer, int index, int count)
		{
			if (m_isFirstField)
				m_isFirstField = false;
			else
				base.Write(m_delimiter);

			bool requireEscape =
				m_escapeMode == CsvEscapeMode.AlwaysEscape
				|| RequiresEscape(buffer, index, count);

			if (requireEscape)
				base.Write(m_escapeCharacter);

			for (int i = index; i < index + count; i++)
			{
				base.Write(buffer[i]);
				if (buffer[i] == m_escapeCharacter)
					base.Write(buffer[i]);
			}

			if (requireEscape)
				base.Write(m_escapeCharacter);
		}

		/// <summary>
		/// Writes a field containing the text representation of a decimal value to the text stream.
		/// </summary>
		/// <param name="value">The decimal value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(decimal value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of an 8-byte floating-point value to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte floating-point value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(double value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of a 4-byte floating-point value to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte floating-point value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(float value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of a 4-byte signed integer value to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte signed integer value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(int value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of a 8-byte signed integer value to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte signed integer value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(long value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of an object to the text stream by calling ToString on that object.
		/// </summary>
		/// <param name="value">The object to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(object value)
		{
			if (value == null)
				WriteField();
			else
				WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing a formatted string, using the same semantics as System.String.Format(System.String,System.Object).
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">An object to write into the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format"/> is null.</exception>
		///   
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.</exception>
		///   
		/// <exception cref="T:System.FormatException">
		/// The format specification in <paramref name="format"/> is invalid -or- The number indicating
		/// an argument to be formatted is less than zero, or larger than or equal to
		/// the number of provided objects to be formatted.</exception>
		public void WriteField(string format, object arg0)
		{
			WriteField(string.Format(format, arg0));
		}

		/// <summary>
		/// Writes a field containing a formatted string, using the same semantics as System.String.Format(System.String,System.Object,System.Object).
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">An object to write into the formatted string.</param>
		/// <param name="arg1">An object to write into the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format"/> is null.</exception>
		///   
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.</exception>
		///   
		/// <exception cref="T:System.FormatException">
		/// The format specification in <paramref name="format"/> is invalid -or- The number indicating
		/// an argument to be formatted is less than zero, or larger than or equal to
		/// the number of provided objects to be formatted.
		public void WriteField(string format, object arg0, object arg1)
		{
			WriteField(string.Format(format, arg0, arg1));
		}

		/// <summary>
		/// Writes a field containing a formatted string, using the same semantics as System.String.Format(System.String,System.Object,System.Object,System.Object).
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">An object to write into the formatted string.</param>
		/// <param name="arg1">An object to write into the formatted string.</param>
		/// <param name="arg2">An object to write into the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format"/> is null.</exception>
		///   
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.</exception>
		///   
		/// <exception cref="T:System.FormatException">
		/// The format specification in <paramref name="format"/> is invalid -or- The number indicating
		/// an argument to be formatted is less than zero, or larger than or equal to
		/// the number of provided objects to be formatted.
		public void WriteField(string format, object arg0, object arg1, object arg2)
		{
			WriteField(string.Format(format, arg0, arg1, arg2));
		}

		/// <summary>
		/// Writes a field containing a formatted string, using the same semantics as System.String.Format(System.String,System.Object[]).
		/// </summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg">The object array to write into the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format"/> is null.</exception>
		///   
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs.</exception>
		///   
		/// <exception cref="T:System.FormatException">
		/// The format specification in <paramref name="format"/> is invalid -or- The number indicating
		/// an argument to be formatted is less than zero, or larger than or equal
		/// to <paramref name="arg"/>.Length.</exception>
		public void WriteField(string format, params object[] arg)
		{
			WriteField(string.Format(format, arg));
		}

		/// <summary>
		/// Writes a field containing a string to the text stream.
		/// </summary>
		/// <param name="value">The string to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(string value)
		{
			if (value == null)
				WriteField();
			else
				WriteField(value.ToCharArray(), 0, value.Length);
		}

		/// <summary>
		/// Writes a field containing the text representation of a 4-byte unsigned integer value to the text stream.
		/// </summary>
		/// <param name="value">The 4-byte unsigned integer value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(uint value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a field containing the text representation of a 8-byte unsigned integer value to the text stream.
		/// </summary>
		/// <param name="value">The 8-byte unsigned integer value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		public void WriteField(ulong value)
		{
			WriteField(value.ToString());
		}

		/// <summary>
		/// Writes a line terminator to the text stream.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">
		/// The <see cref="System.IO.TextWriter"/> is closed.</exception>
		/// <exception cref="System.IO.IOException">
		/// An I/O error occurs.</exception>
		public void WriteRecord()
		{
			base.Write(NewLine);
			m_isFirstField = true;
		}

		/// <summary>
		/// Writes the specified fields followed by a line terminator to the text stream calling ToString on each object.
		/// </summary>
		/// <param name="fields">The fields.</param>
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs. </exception>
		public void WriteRecord(params string[] fields)
		{
			foreach (string field in fields)
				WriteField(field);

			WriteRecord();
		}

		/// <summary>
		/// Writes the specified fields followed by a line terminator to the text stream.
		/// </summary>
		/// <param name="fields">The fields.</param>
		/// <exception cref="T:System.ObjectDisposedException">
		/// The <see cref="T:System.IO.TextWriter"/> is closed. </exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		/// An I/O error occurs. </exception>
		public void WriteRecord(params object[] fields)
		{
			foreach (object field in fields)
				WriteField(field);

			WriteRecord();
		}

		bool RequiresEscape(char[] buffer, int index, int count)
		{
			for (int i = index; i < index + count; i ++)
				if (buffer[i] == m_delimiter || buffer[i] == m_escapeCharacter || buffer[i] == c_lf || buffer[i] == c_cr)
					return true;

			return false;
		}

		const char c_defaultEscapeChar = '"';
		const char c_defaultDelimiter = ',';
		const CsvEscapeMode c_defaultEscapeMode = CsvEscapeMode.AutoEscape;
		const char c_cr = '\r';
		const char c_lf = '\n';

		readonly char m_escapeCharacter;
		readonly char m_delimiter;
		readonly CsvEscapeMode m_escapeMode;

		bool m_isFirstField = true;

		#region Write/WriteLine prevention overrides
		// TODO: Wrap rather than inherit from StreamReader

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(bool value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(char value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(char[] buffer)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(char[] buffer, int index, int count)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(decimal value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(double value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(float value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(int value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(long value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(object value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(string format, object arg0)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(string format, object arg0, object arg1)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

#if !SILVERLIGHT
		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}
#endif

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(string format, params object[] arg)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(string value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(uint value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteField instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteField instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteField instead.", true)]
		public override void Write(ulong value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteField instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine()
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(bool value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(char value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(char[] buffer)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(char[] buffer, int index, int count)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(decimal value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(double value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(float value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(int value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(long value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(object value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(string format, object arg0)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(string format, object arg0, object arg1)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

#if !SILVERLIGHT
		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}
#endif

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(string format, params object[] arg)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(string value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(uint value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		/// <summary>
		/// Writing individual values is not supported. Use WriteRecord instead
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// Writing individual values is not supported. Consider using WriteRecord instead.</exception>
		[Obsolete("Writing individual values is not supported. Use WriteRecord instead.", true)]
		public override void WriteLine(ulong value)
		{
			throw new NotSupportedException("Writing individual values is not supported. Consider using WriteRecord instead.");
		}

		#endregion
	}

	/// <summary>
	/// Represents the mode determining when to escape CSV fields.
	/// </summary>
	public enum CsvEscapeMode
	{
		/// <summary>
		/// Indicates that all fields will always be escaped regardless of their contents.
		/// </summary>
		AlwaysEscape,

		/// <summary>
		/// Indicates that fields will only be escaped if they contain certain characters such as line breaks or delimiters.
		/// </summary>
		AutoEscape
	}
}