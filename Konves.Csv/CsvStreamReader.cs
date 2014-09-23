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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace Konves.Csv
{
	/// <summary>
	/// Implements a <see cref="System.IO.StreamReader"/> that reads CSV fields and records from a byte stream in a particular encoding.
	/// </summary>
	public sealed class CsvStreamReader : StreamReader
	{
		#region == Constructors ==

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified stream.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream)
			: this(stream, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified stream.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, char delimiter, char escape)
			: base(stream)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified file name.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path)
			: this(path, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified file name.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, char delimiter, char escape)
			: base(path)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified stream, with the specified byte order mark detection option.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
			: this(stream, detectEncodingFromByteOrderMarks, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified stream, with the specified byte order mark detection option.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, bool detectEncodingFromByteOrderMarks, char delimiter, char escape)
			: base(stream, detectEncodingFromByteOrderMarks)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified stream, with the specified character encoding.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, Encoding encoding)
			: this(stream, encoding, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified stream, with the specified character encoding.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, Encoding encoding, char delimiter, char escape)
			: base(stream, encoding)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified file name, with the specified byte order mark detection option.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, bool detectEncodingFromByteOrderMarks)
			: this(path, detectEncodingFromByteOrderMarks, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified file name, with the specified byte order mark detection option.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, bool detectEncodingFromByteOrderMarks, char delimiter, char escape)
			: base(path, detectEncodingFromByteOrderMarks)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified file name, with the specified character encoding.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, Encoding encoding)
			: this(path, encoding, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified file name, with the specified character encoding.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, Encoding encoding, char delimiter, char escape)
			: base(path, encoding)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// stream, with the specified character encoding and byte order mark detection option.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// stream, with the specified character encoding and byte order mark detection option.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> is null.</exception>
		public CsvStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, char delimiter, char escape)
			: base(stream, encoding, detectEncodingFromByteOrderMarks)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// file name, with the specified character encoding and byte order mark detection option.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(path, encoding, detectEncodingFromByteOrderMarks, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// file name, with the specified character encoding and byte order mark detection option.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, char delimiter, char escape)
			: base(path, encoding, detectEncodingFromByteOrderMarks)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// stream, with the specified character encoding, byte order mark detection option, and buffer size.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> or <paramref name="encoding">encoding</paramref> is null.</exception>
		///     
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     <paramref name="bufferSize">BufferSize</paramref> is less than or equal to zero.</exception>
		public CsvStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// stream, with the specified character encoding, byte order mark detection option, and buffer size.
		/// </summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///     <paramref name="stream">Stream</paramref> does not support reading.</exception>
		///     
		/// <exception cref="T:System.ArgumentNullException">
		///     <paramref name="stream">Stream</paramref> or <paramref name="encoding">encoding</paramref> is null.</exception>
		///     
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     <paramref name="bufferSize">BufferSize</paramref> is less than or equal to zero.</exception>
		public CsvStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, char delimiter, char escape)
			: base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// file name, with the specified character encoding, byte order mark detection option, and buffer size.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		///   
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize">Buffersize</paramref> is less than or equal to zero. </exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(path, encoding, detectEncodingFromByteOrderMarks, bufferSize, c_defaultDelimiter, c_defaultEscape)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Konves.Csv.CsvStreamReader"/> class for the specified
		/// file name, with the specified character encoding, byte order mark detection option, and buffer size.
		/// </summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters.</param>
		/// <param name="delimiter">The character used to delimit fields.</param>
		/// <param name="escape">The character used to escape fields.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path">Path</paramref> is an empty string ("").</exception>
		///   
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path">Path</paramref> or <paramref name="encoding"/> is null.</exception>
		///   
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		///   
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path">Path</paramref> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		///   
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize">Buffersize</paramref> is less than or equal to zero. </exception>
		[SecuritySafeCritical]
		public CsvStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, char delimiter, char escape)
			: base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
		{
			m_delimiter = delimiter;
			m_escapeCharacter = escape;
		}

		#endregion

		/// <summary>
		/// Gets a value indicating whether the end of the record has been reached when reading individual fields.
		/// This value will be <c>false</c> when ReadRecord is called unless the end of the stream has been reached.
		/// </summary>
		/// <value>
		///   <c>true</c> if the end of the record has been reached; otherwise, <c>false</c>.
		/// </value>
		public bool EndOfRecord
		{
			get { return m_endOfRecord; }
			private set
			{
				m_endOfRecord = value;
				if (m_endOfRecord)
				{
					m_record = null;
					if (base.EndOfStream)
						m_endOfStream = true;
				}
			}
		}

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
		/// Reads a CSV record from the current stream and returns the data as a collection of strings.
		/// </summary>
		/// <returns>
		/// The next CSV record from the input stream, or null if the end of the input stream is reached.
		/// </returns>
		/// <exception cref="T:System.OutOfMemoryException">
		///     There is insufficient memory to allocate a buffer for the returned strings.</exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		///     An I/O error occurs.</exception>
		///     
		/// <exception cref="T:System.FormatException">
		///     The input stream contains mal-formatted CSV data.</exception>
		public string[] ReadRecord()
		{
			if (EndOfStream && EndOfRecord)
				return null;

			return ParseRecord().ToArray();
		}

		/// <summary>
		/// Reads a CSV field from the current stream and returns the data as a string.
		/// </summary>
		/// <returns>
		/// The next CSV field from the input stream, or null if the end of the input stream is reached.
		/// </returns>
		/// <exception cref="T:System.OutOfMemoryException">
		///     There is insufficient memory to allocate a buffer for the returned strings.</exception>
		///   
		/// <exception cref="T:System.IO.IOException">
		///     An I/O error occurs.</exception>
		///     
		/// <exception cref="T:System.FormatException">
		///     The input stream contains mal-formatted CSV data.</exception>
		public string ReadField()
		{
			if (EndOfStream && EndOfRecord)
				return null;

			return ParseField();
		}

		IEnumerable<string> ParseRecord()
		{
			if (EndOfRecord)
			{
				m_record = ReadLine();
				m_recordPosition = 0;
				EndOfRecord = false;
			}

			while (!EndOfRecord)
				yield return ParseField();

			if (m_record != null && m_record[m_record.Length] == m_delimiter)
				yield return string.Empty;
		}

		string ParseField()
		{
			string value = string.Empty;

			if (m_record == null)
			{
				m_record = ReadLine();
				m_recordPosition = 0;

				if (m_record != null)
					EndOfRecord = false;
			}

			bool beforeEsc = true;
			bool inEsc = false;
			bool afterEsc = false;
			bool canEsc = true;
			bool isdblesc = false;

			char d = m_delimiter;
			char e = m_escapeCharacter;

			int datastarts = m_recordPosition;

			// check for null field terminating string
			if (m_recordPosition == m_record.Length)
			{
				value = string.Empty;
				m_record = null;
				EndOfRecord = true;
				return value;
			}

			for (int i = m_recordPosition; i < m_record.Length; i++, m_recordPosition++)
			{
				#region beforeEsc

				if (beforeEsc)
				{
					#region whitespace

					if (Char.IsWhiteSpace(m_record[i]) && !(m_record[i] == c_cr && m_record.Length > i + 1 && m_record[i + 1] == c_lf)) // whitespce (not linebreak)
					{
						// do nothing
					}
					#endregion
					#region escape character

					else if (m_record[i] == e) // escape character
					{
						if (canEsc)
						{
							beforeEsc = false;
							inEsc = true;
							afterEsc = false;

							datastarts = i + 1;
						}
						else
						{
							throw new FormatException(string.Format("Escape character not allowed in un-escaped field: Index {0}", i));
						}
					}
						#endregion
					#region delimiter

					else if (m_record[i] == d) // delimiter
					{
						// reset bools
						beforeEsc = true;
						inEsc = false;
						afterEsc = false;
						canEsc = true;

						// return field 
						value = m_record.Substring(datastarts, i - datastarts);

						// this needs to be dealt with (write test case)
						// add empty record if delimiter is at EOF
						//if (i + 1 == buffer.Length)
						//    yield return string.Empty;

						m_recordPosition++;
						datastarts = m_recordPosition;

						// only return one record
						return value;

					}
					#endregion
					#region linebreak

					else if (m_record[i] == c_cr && m_record.Length > i + 1 && m_record[i + 1] == c_lf) // linebreak
					{
						// reset bools
						beforeEsc = true;
						inEsc = false;
						afterEsc = false;
						canEsc = true;

						// add field to record
						value = m_record.Substring(datastarts, i - datastarts);

						// done reading record
						return value;
					}
					#endregion
					#region EOF

					else if (i == m_record.Length - 1) // EOF
					{
						// return field
						value = m_record.Substring(datastarts, i - datastarts + 1);

						// clear buffer
						m_record = null;
						EndOfRecord = true;
						return value;

					}
					#endregion
					#region other text

					else // other text
					{
						canEsc = false;
					}

					#endregion
				}
					#endregion
				#region inEsc

				else if (inEsc)
				{
					#region first of double escape

					if (!isdblesc && m_record[i] == e && m_record.Length > i + 1 && m_record[i + 1] == e) // first of double escape
					{
						isdblesc = true;
					}
					#endregion
					#region second of double escape

					else if (m_record[i] == e && isdblesc) // second of double escape
					{
						isdblesc = false;
					}
					#endregion
					#region escape character

					else if (m_record[i] == e && !isdblesc) // escape character
					{
						// add field to record
						value = m_record.Substring(datastarts, i - datastarts).Replace(e.ToString() + e.ToString(), e.ToString());

						// set bools
						beforeEsc = false;
						inEsc = false;
						afterEsc = true;
						canEsc = false;

						if (i + 1 == m_record.Length)
						{
							m_record = null;
							this.EndOfRecord = true;
							return value;
						}
					}
					#endregion
					#region EOF

					else if (i == m_record.Length - 1) // EOF
					{
						m_record += Environment.NewLine + base.ReadLine();
					}

					#endregion
				}
					#endregion
				#region afterEsc

				else if (afterEsc)
				{
					#region whitespace

					if (Char.IsWhiteSpace(m_record[i]) && !(m_record[i] == c_cr && m_record.Length > i + 1 && m_record[i + 1] == c_lf)) // whitespce (not linebreak)
					{
					}
					#endregion
					#region delimiter

					else if (m_record[i] == d) // delimiter
					{
						//reset bools
						beforeEsc = true;
						inEsc = false;
						afterEsc = false;
						canEsc = true;

						// add empty record if delimiter is at EOF
						//if (i + 1 == buffer.Length)
						//    yield return string.Empty;

						m_recordPosition++;
						datastarts = m_recordPosition;

						// only return one field
						return value;
					}
					#endregion
					#region linebreak

					else if (m_record[i] == c_cr && m_record.Length > i + 1 && m_record[i + 1] == c_lf) // linebreak
					{
						//reset bools
						beforeEsc = true;
						inEsc = false;
						afterEsc = false;
						canEsc = true;

						this.EndOfRecord = true;

						// done reading record
						return value;
					}
					#endregion
					#region other text

					else // other text
					{
						throw new FormatException(string.Format("Delimiter, line break, or whitespace expected: Index {0}", i));
					}

					#endregion
				}

				#endregion
			}

			value = m_record.Substring(datastarts, m_recordPosition - datastarts);

			if (m_recordPosition == m_record.Length)
				EndOfRecord = true;

			return value;
		}

		public new bool EndOfStream
		{
			get { return !base.EndOfStream ? base.EndOfStream : m_endOfStream; }
		}

		const char c_defaultEscape = '"';
		const char c_defaultDelimiter = ',';
		const char c_cr = '\r';
		const char c_lf = '\n';

		readonly char m_delimiter;
		readonly char m_escapeCharacter;

		string m_record;
		int m_recordPosition;
		bool m_endOfRecord;
		bool m_endOfStream;
	}
}
