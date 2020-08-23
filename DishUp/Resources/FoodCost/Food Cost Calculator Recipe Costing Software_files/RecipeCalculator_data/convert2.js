// This file is copyright 2007 by OnlineConversion.com
// Not for use on any other website.
// Protected by CopyScape, http://www.copyscape.com/about.php

function netscapeKeyPress(e) {
    if (e.which == 13)
        myCon();
}

function microsoftKeyPress() {
    if (window.event.keyCode == 13)
        myCon();
}

if (navigator.appName == 'Netscape') {
    window.captureEvents(Event.KEYPRESS);
    window.onKeyPress = netscapeKeyPress;
}

function ConvertUnits(FromUnit,ToUnit ) {
  var FromVal, ToVal, FromName, ToName, v1;

  v1 = 1;
 
  eval('v1 = parseFloat(' + v1 + ');');
  if (isNaN(v1)) {
	  v1 = 1;
  }

  FromVal = $('#'+FromUnit).val();
  ToVal = $('#'+ToUnit).val();
  FromName = $("#"+FromUnit+" option:selected").text();
  ToName = $('#'+ToUnit+' option:selected').text();

	if ((FromVal == "") || (ToVal == "")){
		$("#FinalValue").text("");
		return;
	}

  FromVal = NewEval(eval(FromVal));
  ToVal = NewEval(eval(ToVal));

  var v3 = eval("FromVal / ToVal");

  // brute force rounding error fix
  if ((FromName == "cup [US]") & (ToName == "ounce [US, liquid]"))  {
	  v3 = eval("8");
  } else if ((FromName == "cup [US]") & (ToName == "pony"))  {
	  v3 = eval("8");
  } else if ((FromName == "quart [US, liquid]") & (ToName == "ounce [US, liquid]"))  {
	  v3 = eval("32");
  } else if ((FromName == "ounce [US, liquid]") & (ToName == "quart [US, liquid]"))  {
	  v3 = eval("1/32");
  }


  var v2 = eval("v1 * v3");
  v2 = NewEval(v2);

  // fix rounding error on frequency page
  if (v2 == "59.999999999"){
	  v2 = "60";
  }
   return v2;
  //alert(space(v1) + " " + FromName + " = " + space(v2) + " " + ToName);
}

function ConvertUnitsFromValues(FromVal, ToVal, FromName, ToName) {
    var v1;
    v1 = 1;

    eval('v1 = parseFloat(' + v1 + ');');
    if (isNaN(v1)) {
        v1 = 1;
    }
        
    if ((FromVal == "") || (ToVal == "")) {
        return;
    }

    FromVal = NewEval(eval(FromVal));
    ToVal = NewEval(eval(ToVal));

    var v3 = eval("FromVal / ToVal");

    // brute force rounding error fix
    if ((FromName == "cup [US]") & (ToName == "ounce [US, liquid]")) {
        v3 = eval("8");
    } else if ((FromName == "cup [US]") & (ToName == "pony")) {
        v3 = eval("8");
    } else if ((FromName == "quart [US, liquid]") & (ToName == "ounce [US, liquid]")) {
        v3 = eval("32");
    } else if ((FromName == "ounce [US, liquid]") & (ToName == "quart [US, liquid]")) {
        v3 = eval("1/32");
    }


    var v2 = eval("v1 * v3");
    v2 = NewEval(v2);

    // fix rounding error on frequency page
    if (v2 == "59.999999999") {
        v2 = "60";
    }
    return v2;
    //alert(space(v1) + " " + FromName + " = " + space(v2) + " " + ToName);
}
function stripBad(string) {
	// If a space is next to two numbers, change to a + sign, for fractions
	// But only if number also contains a / character
	if (string.match(/\//)){
	  while (string.match(/(\d) (\d)/)){
		string = string.replace(/(\d) (\d)/, "$1\+$2");
	  }
	}

    for (var i=0, output='', valid="eE+/*-0123456789.()"; i<string.length; i++)
       if (valid.indexOf(string.charAt(i)) != -1)
          output += string.charAt(i)
    return output;
} 

function space (num)
{
	num = num + '';
	// exit if scientific notation
	if (num.indexOf('e') > -1){ return num; }

	var dec = num.indexOf('.');

	var left, right = '';
	if (dec >= 0)
	{
		left = num.substring(0, dec);
		right = num.substring(dec + 1);
	}
	else
		left = num;

	var new_left = '', new_right = '';
	for (var i = 0; i < right.length; i++)
	{
		new_right += right.charAt(i);
		if (i % 3 == 2 && i != right.length - 1)
			new_right += ' ';
	}
	for (var i = left.length - 1; i >= 0; i--)
	{
		new_left = left.charAt(i) + new_left;
		if ((left.length - 1 - i) % 3 == 2 && i != 0)
			new_left = ' ' + new_left;
	}

	return (dec >= 0) ? new_left + '.' + new_right : new_left;
}



//check whether the char code is numeric or not.
function IsCharCodeNumeric(charCode)
{
    return ((charCode > 47) && (charCode < 58));
}

function NewEval(number)
{
	if (number == undefined) return;

	var strInput = number.toString();
	
	//this is the maximum length of the input string (ignoring decimal and leading zeros) that we consider for our processing.
	//chars after this length (till the exponential part) will simply be replaced by 0.
	var maxLength = 12;
	var charCode;
	var count = 0;	
	var maxLengthIndex = 0;
	var isLeadingZero = true; 
	var currIndex = 0;

   /**********This performs steps 1 and 2 mentioned in the function comments*********/
	for(count = 0; count < strInput.length; count++)
	{
		charCode = strInput.charCodeAt(count);
		//check whether input does not contain any non numeric (0 to 9, "." and ",") characters.
		if (IsCharCodeNumeric(charCode))		
		{
			//ignore any leading zeros
			if ((charCode == 48) && !(isLeadingZero))
			{				
					currIndex++;
			}
			else if (charCode != 48)
			{				
				//if non-zero number is found, then all other following zero's are not leading zeros.
				currIndex++;
				isLeadingZero = false;
			}
		}
		//if the char is neither a "." nor "," then exit 
		else if (!((charCode == 44) || (charCode == 46)))
		{
			//we have encountered a non-numeric character (i.e. other than 0 to 9, ".", ",")
			return FormatOutPut(number);
		}
		
		//if the length has reached the maximum length that we are going to process
		//then save the index and exit the loop.
		if (currIndex == maxLength)  {maxLengthIndex = count; break;}
	}
	
	/************This performst step 3 - Retreive the exponent part *****************/
	//if number is expressed as exponential,then retreive  the exp part alone.
	//e,g, if input is 1.00000000008934e-16, then extract e-16 alone.
	var indexOfe = strInput.indexOf('e');
    var expPart = '';
	count = strInput.length-1;
	if (indexOfe != -1) 
	{	
	    for(count = strInput.length-1; count >-1; count--)
	     {
			expPart = strInput.charAt(count) + expPart;
	        if (count == indexOfe) {break;};
	     }
		count--;
	}

	/************ this performs step 4 & 5 - retreive the unwanted part and round it to zero*****************/
	//if input is 1.00000000008934e-16, then retreive 8934 and convert to 0000.
	var numberPartToIgnore = '';
	for( ;count > -1; count --)
	{
		 charCode = strInput.charCodeAt(count);
	     if (IsCharCodeNumeric(charCode))
		 {
			 numberPartToIgnore = '0' + numberPartToIgnore;
		 }
		 else 
		 {
		     numberPartToIgnore = strInput.charAt(count) + numberPartToIgnore;
		  };

	     if (count == maxLengthIndex) {break;};
	}
	count--;

	/************ this performs step 6 - round off the required part *****************/
	//round off the last digit (that we are going to process) if greater than 5 (which is char code 52)
	//if input is 1.00000000008934e-16 then convert 1.0000000000 to 1.0000000001 (as the next digit is 8 which is > 5)
	var numberPart = '';
	if (strInput.charCodeAt(maxLengthIndex)>52)
	{
		var carryOverFlag = 1;
		for (;count > -1 ; count-- )
		{
			charCode = strInput.charCodeAt(count);
			if (IsCharCodeNumeric(charCode))
			{
				charCode = charCode + carryOverFlag;
				carryOverFlag = 0;				

				//if the number is 9, then reset to 0 and set carry over flag.
				if (charCode == 58)
				{					
					charCode = 48;
					carryOverFlag = 1;
				}
				
				//add to the number 
				numberPart = String.fromCharCode(charCode) + numberPart;
			}
			else
			{
				numberPart = strInput.charAt(count) + numberPart;
			}
		}
		//now we have reached the start of the input string. And now
		//if carry over flag is set, then it means we need to prepend "1".
		if (carryOverFlag == 1)
		{
			numberPart = "1" + numberPart;
		}
	}
	else
	{
		for( ;count>-1; count--)
	    {
			numberPart = strInput.charAt(count) + numberPart;
		}
	}

	/************ validate the rounded number *****************/
	  //now we have all the required parts. 
	  var result = (numberPart + numberPartToIgnore + expPart).valueOf() * 1;
		//a;
	  //if difference between the rounded value and the original value
	  //is in the order of 1e-10, then return the rounded value. else return the original value.
  	  var diff  = Math.abs((result - number)/number); 
	  return ((diff < 1e-10) ? FormatOutPut(result): strInput);
}

/*converts 1e-16 to 1.0e-16 format */
function FormatOutPut(number)
{
	var strInput = number.toString();

	//if no exponent part found, then return the input.
	var indexOfe = strInput.indexOf('e');
	if (indexOfe == -1) return number;
	
	//find if . is present before the exponent part. if found then return the input as it is in proper format.
	var indexOfDot = strInput.substr(0, indexOfe).indexOf('.');
	if (indexOfDot > -1) return number;
	
	//include a . and 0 before the exponent part.
	var result = strInput.substr(0, indexOfe) + '.0' + strInput.substr(indexOfe, strInput.length);
	return result;	
}


//new convertision for recipe
function ConvertUnitsRecipe(FromUnit, ToUnit) {
    var FromVal, ToVal, FromName, ToName, v1;

    v1 = 1;

    //alert($(FromUnit).find(":selected").text());
    //alert($(ToUnit).find(":selected").text());
    eval('v1 = parseFloat(' + v1 + ');');
    if (isNaN(v1)) {
        v1 = 1;
    }

    FromVal = $(FromUnit).val();
    ToVal = $(ToUnit).val();
   // alert(FromVal + "+" + ToVal);
    FromName = $(FromUnit).find(":selected").text();
    ToName = $(ToUnit).find(":selected").text();
   // alert(FromName + "+" + ToName);
    if ((FromVal == "") || (ToVal == "")) {
        $("#FinalValue").text("");
        return;
    }

    FromVal = NewEval(eval(FromVal));
    ToVal = NewEval(eval(ToVal));

    var v3 = eval("FromVal / ToVal");

    // brute force rounding error fix
    if ((FromName == "cup [US]") & (ToName == "ounce [US, liquid]")) {
        v3 = eval("8");
    } else if ((FromName == "cup [US]") & (ToName == "pony")) {
        v3 = eval("8");
    } else if ((FromName == "quart [US, liquid]") & (ToName == "ounce [US, liquid]")) {
        v3 = eval("32");
    } else if ((FromName == "ounce [US, liquid]") & (ToName == "quart [US, liquid]")) {
        v3 = eval("1/32");
    }


    var v2 = eval("v1 * v3");
    v2 = NewEval(v2);

    // fix rounding error on frequency page
    if (v2 == "59.999999999") {
        v2 = "60";
    }
    return v2;
    //alert(space(v1) + " " + FromName + " = " + space(v2) + " " + ToName);
}