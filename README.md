# RFC822DateTimeConverter
Converts an RFC 822 DateTime String To A C# DateTime

<b>Overview</b>
<br>
<br>
This is a simple class that can be used to convert a RFC 822 compliant datetime string into a C# DateTime object.  <a href="https://www.ietf.org/rfc/rfc822.txt" target="_blank">RFC 822</a> describes the date time format used by RSS as indicated in the <a href="http://www.rssboard.org/rss-specification" target="_blank">RSS Specification Document</a>.  This class returns the RFC 822 date as a local datetime but also exposes the underlying Universal Time and other component parts.
<br><br>
<b><u>Usage</u></b>
<br>
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
<br>
<b>What We Are Trying To Solve</b>
<br>
<br>
RFC 822 compliant dates come in a wide variety of formats.  For example, all of the following are valid:
<br>
<br>
<pre>
Mon, 11 Mar 2019 01:57:00 EST
11 Mar 2019 01:57:23 EDT
Mon, 11 Mar 2019 01:57:00 -0500
Mon, 11 Mar 2019 01:57 A
11 Mar 2019 01:57 N
11 Mar 2019 01 A
Mon, 11 Mar 2019 02:00 Z
Mon, 11 Mar 2019 02:00:34 Z
11 Mar 2019 02:00 PST
</pre>
<br>
and so on and so on.  Now if you try to convert these with the DateTime.Parse method, you will get exceptions.  Furthermore, if you try and parse these with the DateTime.ParseExact method, you might get some, but the format collection used to control the formatting is incredibly large.  For example, look at the day of week.  That is optional.  Times don't have to include minutes and seconds for military formats and allow exclusion of seconds in all formats.  The part of RFC 822 that governs the date format is included below.
<br>
<br>

<pre>

5.  DATE AND TIME SPECIFICATION

     5.1.  SYNTAX

     date-time   =  [ day "," ] date time        ; dd mm yy
                                                 ;  hh:mm:ss zzz

     day         =  "Mon"  / "Tue" /  "Wed"  / "Thu"
                 /  "Fri"  / "Sat" /  "Sun"

     date        =  1*2DIGIT month 2DIGIT        ; day month year
                                                 ;  e.g. 20 Jun 82

     month       =  "Jan"  /  "Feb" /  "Mar"  /  "Apr"
                 /  "May"  /  "Jun" /  "Jul"  /  "Aug"
                 /  "Sep"  /  "Oct" /  "Nov"  /  "Dec"

     time        =  hour zone                    ; ANSI and Military

     hour        =  2DIGIT ":" 2DIGIT [":" 2DIGIT]
                                                 ; 00:00:00 - 23:59:59

     zone        =  "UT"  / "GMT"                ; Universal Time
                                                 ; North American : UT
                 /  "EST" / "EDT"                ;  Eastern:  - 5/ - 4
                 /  "CST" / "CDT"                ;  Central:  - 6/ - 5
                 /  "MST" / "MDT"                ;  Mountain: - 7/ - 6
                 /  "PST" / "PDT"                ;  Pacific:  - 8/ - 7
                 /  1ALPHA                       ; Military: Z = UT;
                                                 ;  A:-1; (J not used)
                                                 ;  M:-12; N:+1; Y:+12
                 / ( ("+" / "-") 4DIGIT )        ; Local differential
                                                 ;  hours+min. (HHMM)

     5.2.  SEMANTICS

          If included, day-of-week must be the day implied by the date
     specification.

          Time zone may be indicated in several ways.  "UT" is Univer-
     sal  Time  (formerly called "Greenwich Mean Time"); "GMT" is per-
     mitted as a reference to Universal Time.  The  military  standard
     uses  a  single  character for each zone.  "Z" is Universal Time.
     "A" indicates one hour earlier, and "M" indicates 12  hours  ear-
     lier;  "N"  is  one  hour  later, and "Y" is 12 hours later.  The
     letter "J" is not used.  The other remaining two forms are  taken
     from ANSI standard X3.51-1975.  One allows explicit indication of
     the amount of offset from UT; the other uses  common  3-character
     strings for indicating time zones in North America.

</pre>

Yikes!  In any case, the enclosed project is a simple console app that exercises the 
workings of the class.
<br>
<br>
Have Fun. (and rename the namespaces)


Contact me at <a href="http://sirpenski.com" target="_blank">http://sirpenski.com</a> if you find it necessary.




