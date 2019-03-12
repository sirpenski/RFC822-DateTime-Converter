# RFC822DateTImeConverter
Converts an RFC 822 DateTime String To A C# DateTime

<b>Overview</b>
<br>
<br>
This is a simple class that can be used to convert a RFC 822 compliant datetime string into a C# DateTime object.  <a href="https://www.ietf.org/rfc/rfc822.txt" target="_blank">RFC 822</a> describes the date time format used by RSS as indicated in the <a href="http://www.rssboard.org/rss-specification" target="_blank">RSS Specification Document</a>.  This class returns the RFC 822 date as a local datetime but also exposes the underlying Universal Time and other component parts.
<br>
<br>
<b><u>Usage</u></b>
<br><br>
<pre>


  PFSRfc822DateTimeConverter rfc822Conv = new PFSRfc822DateTimeConverter();
  
  if (rfc822Conv.TryParse("Mon, 11 Mar 2019 01:57:00 EST" out DateTime rslt) 
  {
    console.WriteLine(rslt.ToString("yyyy-MM-dd HH:mm:ss zzz");
  }
  
  try 
  {
    DateTime rt = rfc822Conv.Parse("Mon, 11 Mar 2019 01:57 A");
    Console.WriteLine(rslt.ToString("yyyy-MM-dd HH:mm:ss zzz");
  }
  catch(Exception) {}
  


</pre>
