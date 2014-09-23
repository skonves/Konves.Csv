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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.Csv.Tests
{
	[TestClass]
	public class CsvStreamWriterTests
	{
		[TestMethod]
		public void Standard()
		{
			// Arrange
			string[] data = { "asdf", "wxyz", "qwerty" };
			Stream stream = new MemoryStream();
			string expected = "asdf,wxyz,qwerty" + Environment.NewLine;
			CsvStreamWriter writer = new CsvStreamWriter(stream);

			// Act
			writer.WriteRecord(data);
			writer.Flush();
			stream.Position = 0;
			string result = new StreamReader(stream).ReadToEnd();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Whitespace()
		{
			// Arrange
			string[] data = { "asdf", "  wxyz", " qwerty " };
			Stream stream = new MemoryStream();
			string expected = "asdf,  wxyz, qwerty " + Environment.NewLine;
			CsvStreamWriter writer = new CsvStreamWriter(stream);

			// Act
			writer.WriteRecord(data);
			writer.Flush();
			stream.Position = 0;
			string result = new StreamReader(stream).ReadToEnd();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Quotes()
		{
			// Arrange
			string[] data = { "asdf", "wx\"yz", "qwerty" };
			Stream stream = new MemoryStream();
			string expected = "asdf,\"wx\"\"yz\",qwerty" + Environment.NewLine;
			CsvStreamWriter writer = new CsvStreamWriter(stream);

			// Act
			writer.WriteRecord(data);
			writer.Flush();
			stream.Position = 0;
			string result = new StreamReader(stream).ReadToEnd();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void NewLine()
		{
			// Arrange
			string[] data = { "asdf", "wxyz", "qwerty" + Environment.NewLine };
			Stream stream = new MemoryStream();
			string expected = "asdf,wxyz,\"qwerty" + Environment.NewLine + "\"" + Environment.NewLine;
			CsvStreamWriter writer = new CsvStreamWriter(stream);

			// Act
			writer.WriteRecord(data);
			writer.Flush();
			stream.Position = 0;
			string result = new StreamReader(stream).ReadToEnd();

			// Assert
			Assert.AreEqual(expected, result);
		}
	}
}
