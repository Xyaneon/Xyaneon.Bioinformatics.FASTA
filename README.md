# Xyaneon.Bioinformatics.FASTA

[![License](https://img.shields.io/github/license/Xyaneon/Xyaneon.Bioinformatics.FASTA)][License]
[![NuGet](https://img.shields.io/nuget/v/Xyaneon.Bioinformatics.FASTA.svg?style=flat)][NuGet package]
[![Build Status](https://travis-ci.com/Xyaneon/Xyaneon.Bioinformatics.FASTA.svg?branch=master)][Travis CI]

![Package Icon][icon]

A .NET Standard 2.0 library for working with FASTA genetic sequence files.

This library is very flexible, and offers the following:
- Nucleic and amino acid sequences
- Descriptions and NCBI database identifiers
- Sequential and interleaved FASTA formats
- Single and multiple-sequence FASTA files
- File and stream I/O (both synchronous and asynchronous) and validation,
  with structured data objects
- Sequence string parsing
- Common FASTA file extensions support

## Usage

### Setup

To use this library, you must first install the [NuGet package][NuGet package]
for it.

### Reading FASTA files

This library supports both synchronous and asynchronous file I/O operations.
File reading is done by using methods provided by the `SequenceFileReader`
class.

Below is an example of reading a FASTA file containing a single sequence:

```csharp
using Xyaneon.Bioinformatics.FASTA.IO;

// Snip...

Sequence sequence = SequenceFileReader.ReadSingleFromFile("C:\some\file.fasta");
```

Same task, done asynchronously:

```csharp
using Xyaneon.Bioinformatics.FASTA.IO;

// Snip...

CancellationTokenSource source = new CancellationTokenSource();
CancellationToken token = source.Token;
Sequence sequence = await SequenceFileReader.ReadSingleFromFileAsync("C:\some\file.fasta", token);
```

Reading FASTA files containing multiple sequences is also supported:

```csharp
using Xyaneon.Bioinformatics.FASTA.IO;

// Snip...

IEnumerable<Sequence> sequences = SequenceFileReader.ReadMultipleFromFile("C:\some\file.fasta");

// Or:

CancellationTokenSource source = new CancellationTokenSource();
CancellationToken token = source.Token;
IEnumerable<Sequence> sequences = await SequenceFileReader.ReadMultipleFromFileAsync("C:\some\file.fasta", token);
```

### The `Sequence` class

Individual sequences in FASTA files are represented by `Sequence` instances.

Each `Sequence` has a `Header`and `Data` property, which are of types `Header`
and `IActualSequence`. The `Header` contains one or more `HeaderItem`s, which
are the description and any database identifiers. `Data` is either an
`AminoAcidSequence` or a `NucleicAcidSequence`, and contains the sequence data
with the standard characters.

These classes are meant to be immutable; create copies with the new property
values to make changes.

Here is an example for how to create a nucleic acid sequence manually with
this library:

```csharp
using Xyaneon.Bioinformatics.FASTA.ActualSequences;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

// Snip...

// Create a local identifier.
var identifier = new LocalIdentifier("123");
// Create the sequence data.
var data = new NucleicAcidSequence("ATCGAAAA");
// Assemble.
var sequence = new Sequence(new Header(identifier), data);

// Getting back a string representation of how this sequence will actually
// appear in a FASTA file. Sequential and interleaved formats are both
// supported.
// This will return:
// >lcl|123\nATCGAAAA
string sequential = string.join('\n', sequence.ToSequentialLines());

// You can also create a Sequence identical to the one above by parsing:
var sameSequence = Sequence.Parse(sequential);
```

### Writing FASTA files

File writing is done by using methods provided by the `SequenceFileWriter`
class.

```csharp
using Xyaneon.Bioinformatics.FASTA.IO;

// Snip...

// Writing a single sequence to an interleaved file synchronously, with a line
// length of 60 characters.
SequenceFileWriter.WriteToInterleavedFile(sequence, "C:\some\file.fasta", 60);

// Writing multiple sequences to a sequential file asynchronously.
IEnumerable<Sequence> sequences = new Sequence[] {
    sequence1,
    sequence2
}.ToList();
SequenceFileWriter.WriteToSequentialFileAsync(sequences, "C:\some\file.fasta", token);
```

## License

This library is free and open-source software provided under the MIT license.
Please see the [LICENSE.txt][License] file for details.

[icon]: https://github.com/Xyaneon/Xyaneon.Bioinformatics.FASTA/blob/master/Xyaneon.Bioinformatics.FASTA/images/icon.png
[License]: https://github.com/Xyaneon/Xyaneon.Bioinformatics.FASTA/blob/master/LICENSE.txt
[NuGet package]: https://www.nuget.org/packages/Xyaneon.Bioinformatics.FASTA/
[Travis CI]: https://travis-ci.com/Xyaneon/Xyaneon.Bioinformatics.FASTA
