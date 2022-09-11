
mergeInto(LibraryManager.library, {

    SendJSONToPage: function (text)
    {
        //sendMessageToPage
        var convertedText = Pointer_stringify(text);

        //embeded into page
        receiveMessageFromUnity(convertedText);
    },

    // Function with the text param
   PassTextParam: function (text) 
   {
      // Convert bytes to the text
      var convertedText = Pointer_stringify(text);

      // Show a message as an alert
      window.alert("Przekazales wiadomosc: " + convertedText);
   },

   GetJSONFromPage: function () 
   {
      // Define text value
      var textToPass = "To wiadomosc ze strony";

      var bufferSize = lengthBytesUTF8(textToPass) + 1;
      var buffer = _malloc(bufferSize);

      // Convert text
      stringToUTF8(textToPass, buffer, bufferSize);

      // Return text value
      return buffer;
   }
});