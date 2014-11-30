<html>

<head>
<meta http-equiv=Content-Type content="text/html; charset=gb2312">
<meta name=Generator content="Microsoft Word 12 (filtered)">
<style>

</style>

</head>

<body lang=EN-US link=blue vlink=purple>

<div class=WordSection1>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:24.0pt;
font-family:"Times New Roman","serif"'>Cache Service Project</span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>Cache Service Project is a backend
project that provide caching for function call with expiration. The cache will
expire after certain duration of idleness.</span></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>It also includes a hosted simple WCF web
services and a simple web client to show how to use the cache service, as well
as a test sub-project.</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:18.0pt;
font-family:"Times New Roman","serif"'>Problem</span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>Some function calls are heavy, (i.e.
they are time consuming to expect a return) while they are called frequently
with similar parameters across the server or servers. The performance of such
function calls is low.</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:18.0pt;
font-family:"Times New Roman","serif"'>Solution</span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>Caching is a perfect solution for such
situation. &nbsp;This project maps function calls to caching services like
memory cache, database cache, etc.</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:18.0pt;
font-family:"Times New Roman","serif"'>Sub-Projects</span></b></p>

<p class=MsoListParagraphCxSpFirst style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>CacheServiceLib</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Cache Service
Implementation. This is the most important part of the project.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>A common
Structure to apply the service</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>CacheServiceWeb</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>A WCF Web Service
to publish a sample use case that applies cache service</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>CacheServiceWeb</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>A sample web
page to consume the web service</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>TestCache</span></p>

<p class=MsoListParagraphCxSpLast style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Unit Test for
Cache Service</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:18.0pt;
font-family:"Times New Roman","serif"'>CacheServiceLib</span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>The basic ideas are:</span></p>

<p class=MsoListParagraphCxSpFirst style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Inject
caching service to function call flow using reflection (CacheProxy class)</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>This makes
applying the caching service easy. Users just make call to functions and let
caching service to initialize or fetch cached data.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>The drawback is
more work to config and setup the function calls in code.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Caching
objects in map structure </span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>A key - value
pair is the caching storage interface.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>This structure
supports memory caching, file caching, database caching, etc using a proper
serialization method.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Map has O(1)
operation complicity, so the performance is good, but space usage may be
consuming.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Double
Linked List to order the cache values by access timestamp</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>O(1) operation
to update the access timestamp (MemCache class)</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Cache
Expiration</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Cache expiration
is not implemented with a periodical update time check. It only removes expired
cache values after Set function call, and return null if Get function finds an
expired cache value.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>the advantage of
this implementation is less check work if set function is not called often
(which is the case for caching with expiration) and less complicity without one
more thread.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Trade-off is some
supposed free spaces may be released late.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Concurrency
&amp; Multi-threading</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>This service is
thread safe, and can handle high volume concurrency requests.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>A important
performance drawback was multiple access before a cache value initialization.
Multiple initializations will happen in such case.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>A key grouped
lock is implemented to queue the requests for the same key concurrently. (CacheKeyLock
class)</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Testing</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>TestCache
Project is to do the unit test. It provides 3 functional test cases and 1
running test cases in multi-threads situation</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Logging</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Log4net is used
to log information and store multi-threading test results.</span></p>

<p class=MsoListParagraphCxSpMiddle style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;line-height:normal'><span
style='font-size:12.0pt;font-family:Symbol'>·<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span style='font-size:12.0pt;font-family:"Times New Roman","serif"'>Left-out</span></p>

<p class=MsoListParagraphCxSpLast style='line-height:normal'><span
style='font-size:12.0pt;font-family:"Times New Roman","serif"'>This is a simple
caching implementation. lots of features can be added, like cache space control.
A useful feature will be caching across servers.</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:18.0pt;
font-family:"Times New Roman","serif"'>Links</span></b></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>Client website (Cache expiration is 1
minute)</span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>http://zhuowang10-001-site1.smarterasp.net/CacheAccessWebSite/</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>Web Service entry: </span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>http://zhuowang10-001-site1.smarterasp.net/CacheServiceWeb/Service.svc</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>Linkedin profile</span></b></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>www.linkedin.com/pub/zhuo-wang/74/126/953/</span></p>

<p class=MsoNormal style='line-height:normal'><span style='font-size:12.0pt;
font-family:"Times New Roman","serif"'>&nbsp;</span></p>

<p class=MsoNormal style='line-height:normal'><b><span style='font-size:18.0pt;
font-family:"Times New Roman","serif"'>&nbsp;</span></b></p>

<p class=MsoNormal>&nbsp;</p>

</div>

</body>

</html>
