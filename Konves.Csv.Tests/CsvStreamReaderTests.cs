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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.Csv.Tests
{
	[TestClass]
	public class CsvStreamReaderTests
	{
		[TestMethod]
		public void TrailingLineBreak()
		{
			// Arrange
			string data = "asdf" + Environment.NewLine;
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord = "asdf";

			// Act
			string actualRecord = reader.ReadField();

			// Assert
			Assert.AreEqual(expectedRecord, actualRecord);
			Assert.IsTrue(reader.EndOfRecord);
			Assert.IsTrue(reader.EndOfStream);
		}

		[TestMethod]
		public void TrailingSpace()
		{
			// Arrange
			string data = "asdf, ";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord1 = "asdf";
			string expectedRecord2 = " ";

			// Act
			string actualRecord1 = reader.ReadField();
			Assert.IsFalse(reader.EndOfRecord);

			string actualRecord2 = reader.ReadField();

			// Assert
			Assert.IsTrue(reader.EndOfRecord);
			Assert.IsTrue(reader.EndOfStream);
			Assert.AreEqual(expectedRecord1, actualRecord1);
			Assert.AreEqual(expectedRecord2, actualRecord2);
		}

		[TestMethod]
		public void UnreadTrailingDelimiter()
		{
			// Arrange
			string data = "asdf,";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord = "asdf";
			bool expectedEndOfRecord = false;
			bool expectedEndOfStream = false;

			// Act
			string actualRecord = reader.ReadField();
			bool actualEndOfRecord = reader.EndOfRecord;
			bool actualEndOfStream = reader.EndOfStream;

			// Assert
			Assert.AreEqual(expectedRecord, actualRecord);
			Assert.AreEqual(expectedEndOfRecord, actualEndOfRecord);
			Assert.AreEqual(expectedEndOfStream, actualEndOfStream);
		}

		[TestMethod]
		public void ReadTrailingDelimiter()
		{
			// Arrange
			string data = "asdf,";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord1 = "asdf";
			string expectedRecord2 = string.Empty;
			bool expectedEndOfRecord = true;
			bool expectedEndOfStream = true;

			// Act
			string actualRecord1 = reader.ReadField();
			string actualRecord2 = reader.ReadField();
			bool actualEndOfRecord = reader.EndOfRecord;
			bool actualEndOfStream = reader.EndOfStream;

			// Assert
			Assert.AreEqual(expectedRecord1, actualRecord1);
			Assert.AreEqual(expectedRecord2, actualRecord2);
			Assert.AreEqual(expectedEndOfRecord, actualEndOfRecord);
			Assert.AreEqual(expectedEndOfStream, actualEndOfStream);
		}

		[TestMethod]
		public void SingleSpaceLine()
		{
			// Arrange
			string data = "asdf,qwerty" + Environment.NewLine + " ";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord1 = "asdf";
			string expectedRecord2 = "qwerty";
			string expectedRecord3 = " ";

			// Act
			string actualRecord1 = reader.ReadField();
			Assert.IsFalse(reader.EndOfRecord);
			string actualRecord2 = reader.ReadField();
			Assert.IsTrue(reader.EndOfRecord);
			string actualRecord3 = reader.ReadField();
			Assert.IsTrue(reader.EndOfRecord);

			// Assert
			Assert.IsTrue(reader.EndOfStream);
			Assert.AreEqual(expectedRecord1, actualRecord1);
			Assert.AreEqual(expectedRecord2, actualRecord2);
			Assert.AreEqual(expectedRecord3, actualRecord3);
		}

		[TestMethod]
		public void UnescapedWhitespace()
		{
			// Arrange
			string data = "asdf, ,qwerty";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord1 = "asdf";
			string expectedRecord2 = " ";
			string expectedRecord3 = "qwerty";

			// Act
			string actualRecord1 = reader.ReadField();
			string actualRecord2 = reader.ReadField();
			string actualRecord3 = reader.ReadField();

			// Assert
			Assert.AreEqual(expectedRecord1, actualRecord1);
			Assert.AreEqual(expectedRecord2, actualRecord2);
			Assert.AreEqual(expectedRecord3, actualRecord3);
		}

		[TestMethod]
		public void ExtraWhitespace()
		{
			// Arrange
			string data = "asdf, \"wx" + Environment.NewLine + "yz\" ,qwerty";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord1 = "asdf";
			string expectedRecord2 = "wx" + Environment.NewLine + "yz";
			string expectedRecord3 = "qwerty";

			// Act
			string actualRecord1 = reader.ReadField();
			string actualRecord2 = reader.ReadField();
			string actualRecord3 = reader.ReadField();

			// Assert
			Assert.AreEqual(expectedRecord1, actualRecord1);
			Assert.AreEqual(expectedRecord2, actualRecord2);
			Assert.AreEqual(expectedRecord3, actualRecord3);
		}

		[TestMethod]
		public void EscapedQuotes()
		{
			// Arrange
			string data = "asdf, \"wx\"\"yz\" ,qwerty";
			Encoding encoding = Encoding.UTF8;
			Stream stream = new MemoryStream(encoding.GetBytes(data));
			CsvStreamReader reader = new CsvStreamReader(stream, encoding);
			string expectedRecord1 = "asdf";
			string expectedRecord2 = "wx\"yz";
			string expectedRecord3 = "qwerty";

			// Act
			string actualRecord1 = reader.ReadField();
			string actualRecord2 = reader.ReadField();
			string actualRecord3 = reader.ReadField();

			// Assert
			Assert.AreEqual(expectedRecord1, actualRecord1);
			Assert.AreEqual(expectedRecord2, actualRecord2);
			Assert.AreEqual(expectedRecord3, actualRecord3);
		}
	}
}
