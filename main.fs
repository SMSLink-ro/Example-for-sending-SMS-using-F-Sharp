//
//  Example tested with F# Compiler for F# 4.5 (Open Source Edition)
//

open System.Text
open System.IO
open System.Net

let url = "https://secure.smslink.ro/sms/gateway/communicate/index.php"

let req = HttpWebRequest.Create(url) :?> HttpWebRequest 
req.ProtocolVersion <- HttpVersion.Version10
req.Method <- "POST"

//
//  Get your SMSLink / SMS Gateway Connection ID and Password from 
//    https://www.smslink.ro/get-api-key/
//

let postBytes = Encoding.ASCII.GetBytes("connection_id=...My Connection ID ...&password=... My Connection Password ...&to=07xyzzzzzz&message=Test Message")
req.ContentType <- "application/x-www-form-urlencoded";
req.ContentLength <- int64 postBytes.Length

let reqStream = req.GetRequestStream() 
reqStream.Write(postBytes, 0, postBytes.Length);
reqStream.Close()

let resp = req.GetResponse() 
let stream = resp.GetResponseStream() 
let reader = new StreamReader(stream) 
let html = reader.ReadToEnd()

printfn "%s" html
