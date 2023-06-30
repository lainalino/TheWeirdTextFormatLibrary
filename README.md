# The Weird Format Text

##Overview

Library to encode 4-character bundles by scrambling them into 32-bit integer values for transmission and then reversing the operation at the receiving end 
to reconstitute the original text.

### **Stacks**:

The implementation is developed in C#
Use of the .Net 6 framework

### **Encoding the text**

In the **Global** class were added:
*sCharacterSize*: which represents the size of the character to be encoded.
*sBinarySize*: bit size;
*sMaxSize*: binary size (*sCharacterSize* x *sBinarySize*) .

The **Encode** method of the **ATextDecodeEncode** class encodes a 4-character string. The string is inverted to then be converted into binaries. 
If the input size is smaller than the *sCharacterSize*, "0" is added to the left of the binary until the expected size.
After this conversion, this new string is transformed into an array [*sCharacterSize*,*sBinarySize*]. 
Another matrix is created in which the rows of that matrix will be columns of the new matrix, and the columns will be the rows 
(array[*sBinarySize*, *sCharacterSize*], to then be converted to a string. 
The result of the encoding is the conversion of this new binary to a decimal number.

**Example 1:**

textToEncode: lain (when inverting: nial)
binary: 01101110011010010110000101101100
array [sCharacterSize,sBinarySize]:
									[0 1 1 0 1 1 1 0]
									[0 1 1 0 1 0 0 1]
									[0 1 1 0 0 0 0 1]
									[0 1 1 0 1 1 0 0]

Converting to array [sBinarySize,sCharacterSize]
									[0 0 0 0]
									[1 1 1 1]
									[1 1 1 1]
									[0 0 0 0]
									[1 1 0 1]
									[1 0 0 1]
									[1 0 0 0]
									[0 1 1 0]

The binary will be: 00001111111100001101100110000110
The decimal: **267442566**

**Example 2:**

textToEncode: laina lino 
There are 3 words: "lain", "a li", "no"

First Word: lain (when inverting: nial)
	binary: 01101110011010010110000101101100
	array [sCharacterSize,sBinarySize]:

	[0 1 1 0 1 1 1 0]
	[0 1 1 0 1 0 0 1]
	[0 1 1 0 0 0 0 1]
	[0 1 1 0 1 1 0 0]

	Converting to array [sBinarySize,sCharacterSize]
	[0 0 0 0]
	[1 1 1 1]
	[1 1 1 1]
	[0 0 0 0]
	[1 1 0 1]
	[1 0 0 1]
	[1 0 0 0]
	[0 1 1 0]
	
	The binary will be: 00001111111100001101100110000110
	The decimal: **267442566**
	
Second Word: "a li" (when inverting: il a)
	binary: 01101001011011000010000001100001
	array [sCharacterSize,sBinarySize]:

	[0 1 1 0 1 0 0 1]
	[0 1 1 0 1 1 0 0]
	[0 0 1 0 0 0 0 0]
	[0 1 1 0 0 0 0 1]

	Converting to array [sBinarySize,sCharacterSize]
	[0 0 0 0]
	[1 1 0 1]
	[1 1 1 1]
	[0 0 0 0]
	[1 1 0 0]
	[0 1 0 0]
	[0 0 0 0]
	[1 0 0 1]
	The binary will be: 00001101111100001100010000001001
	The decimal: **233882633**

Third Word: "no" (when inverting: no)
	"no" is less than *sCharacterSize* (in this example, 4), so the binary will be complete with "0"
	binary: 00000000000000000110111101101110
	array [sCharacterSize,sBinarySize]:

	[0 0 0 0 0 0 0 0]
	[0 0 0 0 0 0 0 0]
	[0 1 1 0 1 1 1 1]
	[0 1 1 0 1 1 1 0]

	Converting to array [sBinarySize,sCharacterSize]
	[0 0 0 0]
	[0 0 1 1]
	[0 0 1 1]
	[0 0 0 0]
	[0 0 1 1]
	[0 0 1 1]
	[0 0 1 1]
	[0 0 1 0]
	
	The binary will be: 00001101111100001100010000001001
	The decimal: **53490482**
	


### **Decoding the text**

The **Decode** method of the **ATextDecodeEncode** class decodes a decimal.
The first step is convert the decimal do binary.  The string is inverted to then be converted into binary. 
After this conversion, this binary is transformed into an array [*sBinarySize*,*sCharacterSize*]. 
Another matrix is created in which the rows of that matrix will be columns of the new matrix, and the columns will be the rows 
(array[*sCharacterSize*, *sBinarySize*], to then be converted to a string. 
The result of the encoding is the conversion of this new binary to a text.


**Example 1:**

textToDecode: 267442566,233882633,53490482 
There are 3 decimals: 267442566,233882633,53490482

First Decimal: 267442566 
	binary: 00001111111100001101100110000110
	array [sBinarySize, sCharacterSize]:

	[0 0 0 0]
	[1 1 1 1]
	[1 1 1 1]
	[0 0 0 0]
	[1 1 0 1]
	[1 0 0 1]
	[1 0 0 0]
	[0 1 1 0]
	
	Converting to array [sCharacterSize,sBinarySize]
	
	[0 1 1 0 1 1 1 0]
	[0 1 1 0 1 0 0 1]
	[0 1 1 0 0 0 0 1]
	[0 1 1 0 1 1 0 0]
		
	The binary will be: 01101110011010010110000101101100
	The text: nial (When invert: lain
	
Second Decimal: 233882633 
	binary: 00001101111100001100010000001001
	array [sBinarySize, sCharacterSize]:

	[0 0 0 0]
	[1 1 0 1]
	[1 1 1 1]
	[0 0 0 0]
	[1 1 0 0]
	[0 1 0 0]
	[0 0 0 0]
	[1 0 0 1]
	
	Converting to array [sCharacterSize,sBinarySize]
	
	[0 1 1 0 1 0 0 1]
	[0 1 1 0 1 1 0 0]
	[0 0 1 0 0 0 0 0]
	[0 1 1 0 0 0 0 1]
		
	The binary will be: 01101001011011000010000001100001
	The text: il a (When invert: a li

Third Decimal: 53490482 
	binary: 00000011001100000011001100110010
	array [sCharacterSize,sBinarySize]:

	[0 0 0 0 0 0 1 1]
	[0 0 1 1 0 0 0 0]
	[0 0 1 1 0 0 1 1]
	[0 0 1 1 0 0 1 0]

	Converting to array [sBinarySize,sCharacterSize]
	[0 0 0 0]
	[0 0 0 0]
	[0 1 1 1]
	[0 1 0 0]
	[0 0 0 0]
	[0 0 0 0]
	[1 0 1 1]
	[1 0 1 0]
	
	The binary will be: 00000000000000000110111101101110
	The text: on (When invert: no)
	
When the results are append after invert: laina lino