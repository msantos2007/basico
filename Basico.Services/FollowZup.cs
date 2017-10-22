using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

//Requires RestSharp: https://www.nuget.org/packages/RestSharp/
using RestSharp;

//Requires Bouncy Castle - https://www.nuget.org/packages/BouncyCastle/
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

namespace Basico.Services
{
    public class fzup_param
    {
        public fzup_param()
        {
            this.FZUP_COMMAND = "";
            this.FZUP_HOURS = 0;
            this.FZUP_LASTSEQ = 0;
            this.FZUP_MSGTEXT = "";
            this.FZUP_MSGURL = "";
            this.FZUP_SUBSCODE = "";
            this.FZUP_USER = "";
        }

        public string FZUP_COMMAND { get; set; }
        public int? FZUP_LASTSEQ { get; set; }
        public string FZUP_USER { get; set; }
        public string FZUP_SUBSCODE { get; set; }
        public int? FZUP_HOURS { get; set; }
        public string FZUP_MSGTEXT { get; set; }
        public string FZUP_MSGURL { get; set; }
    }

    public class fzup_return
    {
        public string fzup_retcode { get; set; }
        public int fzup_lastseq { get; set; }
        public string fzup_retframe3 { get; set; }
    }

    //For responses from user
    public class fzup_response
    {
        public string fzupidchannel { get; set; }
        public string fzupresponse { get; set; }
    }

    public class followZup
    {
        public string fzup_channel = "capkbnhjbjr9";
        public string fzup_pubkey = "";
        public string fzup_pubkey64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUlJQklqQU5CZ2txaGtpRzl3MEJBUUVGQUFPQ0FROEFNSUlCQ2dLQ0FRRUF0ZXE2eVdRd3N0SkZPd3ZseE9mcAo4TVdvNnF0Z0Z2NVpyRGV4R01pdzdtZ0ZYZ3lQTGFwYzYyMUU4VHNZVWE4M0Ntdk1iRGtvSE1SU01mZ3JzYktvCnNiczh5K0ZRZTRSOW9XSklKTmU0NUVSYkh0UkZjNFQ1ZmtYTnlrSGNsa0xseitVUHAySDlkc0RaT0ZwOWFoWFMKRTVNNk81aDVhV3I5YzExK2FhTndtcFBPSWRCaTkxeGc4Nk0wZzlLUXg5dC9Kc0ZtZVFzN1ZvaTNFdjJHL1BKYQo1VHZhKy9FQzFXS3ByNWliWk9sS1MyWGkvZU1VWSs1b0RyZXNJQ0l2ZURCcmU4WFJubHVkcldidXRsazY4L3JqCkl0TGQ1MFM5U0EydndxRFcxR1ZVMDBld2NtVTk5QXdtSW00Q3lzYm5lSDg1UWJpZVBRZUt0ckdXby85dlkyNG4KSHdJREFRQUIKLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0tCg==";
        public int fzup_lastseq = 0;

        //public string fzup_channel = "captpfh9qcu2";
        //public string fzup_pubkey = "";
        //public string fzup_pubkey64 = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUlJQklqQU5CZ2txaGtpRzl3MEJBUUVGQUFPQ0FROEFNSUlCQ2dLQ0FRRUF3OGl3Y2ZaZjFRSEE4bmZLcm5HNwp3ZTJPMkZRbFFuZW8rNVFqOURXSzhteGhCQzQ4QmRFL2NneXdtYTJENFhxbEhJc1ozYzVBQmRMcGlFN0tZc1BBClRkRTdYVCtGOWxZaDdZejhueTRHTG4ydGppMDYyNG45WjEvS3hGM2RJZzlad25jak5MSjR3UFpFTXh2STI2MmQKVkhwRG9XSFJpOVVORkV6bDR3US9LQlMxMkMyZWhqNVlnMkhraXJ6cUZpdENkeGJscjBMVkJOdk42S3NlUWxkawpscEhHdXpVcC9ScmZwa1ZlTkMxdnFFV1k4Z2R6TTlhV3B5Tnk1dkdrSFdOYTVTRDJJYjNBNzdtRzNwZlVnZGJKCkpvcVh3NjdWU2RjSk92VlEweVQ1czFkdTNiUjUrb2xRNGp6TzRkK0x1dFZYQ2FRN2FJVW9aSkhVT0hDN3dIZkoKb1FJREFRQUIKLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0tCg==";
        //public int fzup_lastseq = 0;

        public followZup()
        {
            byte[] data = Convert.FromBase64String(fzup_pubkey64);
            this.fzup_pubkey = Encoding.UTF8.GetString(data);
        }

        public string decrypt(string fzup_encrypt64)
        {
            try
            {
                return RsaDecryptWithPublic(fzup_encrypt64, this.fzup_pubkey);
            }
            catch { }

            try
            {
                return RsaDecryptWithPublic(System.Uri.UnescapeDataString(fzup_encrypt64), this.fzup_pubkey);
            }
            catch { }

            return "";
        }
		
        public string[] submit(fzup_param mensagem)
        {
            int seq = 0;
            mensagem.FZUP_LASTSEQ = mensagem.FZUP_LASTSEQ ?? seq;

            int hrs = 0;
            mensagem.FZUP_HOURS = mensagem.FZUP_HOURS ?? hrs;

            //mensagem.FZUP_MSGURL = "http://www.website.com/ofertas";

            byte[] b_FZUP_MSGTEXT = Encoding.UTF8.GetBytes(mensagem.FZUP_MSGTEXT);
            byte[] b_FZUP_MSGURL = Encoding.UTF8.GetBytes(mensagem.FZUP_MSGURL);
            mensagem.FZUP_MSGTEXT = Convert.ToBase64String(b_FZUP_MSGTEXT);
            mensagem.FZUP_MSGURL = Convert.ToBase64String(b_FZUP_MSGURL);

            string xml = "";
            string cmd = mensagem.FZUP_COMMAND;

            if (cmd == "chck")
            {
                xml = "";
                xml += "<usr>" + mensagem.FZUP_USER + "</usr>";
                xml += "<sub>" + mensagem.FZUP_SUBSCODE + "</sub>";
            }
            else
            {
                if (cmd == "smsg")
                {
                    xml = "";
                    xml += "<usr>" + mensagem.FZUP_USER + "</usr>";
                    xml += "<hrs>" + mensagem.FZUP_HOURS + "</hrs>";
                    xml += "<msg>" + mensagem.FZUP_MSGTEXT + "</msg>";
                    xml += "<url>" + mensagem.FZUP_MSGURL + "</url>";
                }
                else
                {
                    return new string[] { "6103", this.fzup_lastseq.ToString(), @"<?xml version=""1.0"" encoding=""utf-8""?><followzup></followzup>" };
                };
            }

            if (mensagem.FZUP_LASTSEQ > 0) fzup_lastseq = (int)mensagem.FZUP_LASTSEQ;

            string fzup_RetCode = "";
            string fzup_frame1 = "";
            byte[] fzup_frame2;
            string fzup_frame3 = "";
            byte[] fzup_key1;
            byte[] fzup_key2;
            string fzup_key3 = null;

            string fzup_retframe4 = "";


            do
            {
                this.fzup_lastseq++;

                fzup_frame1 = "";
                fzup_frame1 += @"<?xml version=""1.0"" encoding=""utf-8""?>";
                fzup_frame1 += "<followzup>";
                fzup_frame1 += "<com>" + mensagem.FZUP_COMMAND + "</com>";
                fzup_frame1 += "<seq>" + this.fzup_lastseq + "</seq>";
                fzup_frame1 += xml;
                fzup_frame1 += "</followzup>";

                //AES - Encryption
                //Padding with spaces to complete 16 bytes block, because of "rijAlg.Padding = PaddingMode.None"
                //IV - Important: ASCII "null" char "0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0"
                byte[] n_plain = Encoding.UTF8.GetBytes(fzup_frame1);
                int n_length = n_plain.Length;
                int p_length = n_length + (15 - ((n_length - 1) % 16));
                byte[] b_plain = Enumerable.Repeat((byte)0x20, p_length).ToArray();
                Array.Copy(n_plain, 0, b_plain, 0, n_length);

                fzup_frame1 = Encoding.UTF8.GetString(b_plain);

                RijndaelManaged rijAlg = new RijndaelManaged();

                rijAlg.BlockSize = 128;
                rijAlg.KeySize = 192;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.None;
                rijAlg.GenerateKey();

                byte[] null_char_byte = Enumerable.Repeat((byte)0x00, 16).ToArray();
                string null_char_strg = Encoding.UTF8.GetString(null_char_byte);

                rijAlg.IV = null_char_byte;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(fzup_frame1);
                        }
                        fzup_frame2 = msEncrypt.ToArray();
                    }
                }

                fzup_key1 = rijAlg.Key;

                //RSA - Encryption 
                string publicKey_noHeader = this.fzup_pubkey;
                publicKey_noHeader = publicKey_noHeader.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "");

                RSACryptoServiceProvider RSAenc = DecodeX509PublicKey(Convert.FromBase64String(publicKey_noHeader));

                fzup_key2 = RSAenc.Encrypt(fzup_key1, false);


                fzup_key3 = Convert.ToBase64String(fzup_key2);
                fzup_frame3 = Convert.ToBase64String(fzup_frame2);

                //RestSharp
                IRestResponse response = null;
                var client = new RestClient("http://www.followzup.com");
                var request = new RestRequest("/wschannel", Method.POST);

                client.UserAgent = "wschannel: " + this.fzup_channel;

                request.AddParameter("id", this.fzup_channel);
                request.AddParameter("key", fzup_key3);
                request.AddParameter("frame", fzup_frame3);
                response = client.Execute(request);

                System.Xml.XmlDocument responsexml = new System.Xml.XmlDocument();
                responsexml.LoadXml(response.Content.ToString());

                System.Xml.XmlNodeList xn = responsexml.SelectNodes("/followzup");
                fzup_RetCode = xn[0].SelectSingleNode("retcode").InnerText;
                string fzup_retframe1 = xn[0].SelectSingleNode("retframe").InnerText;
                string fzup_retframe2 = "";


                //AES - Decryption
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(fzup_retframe1)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            fzup_retframe2 = srDecrypt.ReadToEnd();
                        }
                    }
                }

                if (fzup_RetCode == "6101")
                {
                    System.Xml.XmlDocument fzup_retframe3 = new System.Xml.XmlDocument();
                    fzup_retframe3.LoadXml(fzup_retframe2);

                    System.Xml.XmlNodeList xL = fzup_retframe3.SelectNodes("/followzup");

                    this.fzup_lastseq = Convert.ToInt32(xL[0].SelectSingleNode("seq").InnerText);
                }

                fzup_retframe4 = fzup_retframe2;

            } while (fzup_RetCode == "6101");

            return new string[] { fzup_RetCode, this.fzup_lastseq.ToString(), fzup_retframe4 };
        }

        public string RsaDecryptWithPublic(string base64Input, string publicKey)
        {
            //Bouncy Castle
            var bytesToDecrypt = Convert.FromBase64String(base64Input);

            var decryptEngine = new Pkcs1Encoding(new RsaEngine());

            using (var txtreader = new StringReader(publicKey))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();

                decryptEngine.Init(false, keyParameter);
            }

            var decrypted = Encoding.UTF8.GetString(decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));

            return decrypted;
        }

        public RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
        {
            //https://stackoverflow.com/questions/11506891/how-to-load-the-rsa-public-key-from-file-in-c-sharp
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(x509key);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }
        }

        public bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }
    }
}