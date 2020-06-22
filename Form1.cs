using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using NAudio.Wave;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace VoiceMessage
{
    public partial class Form1 : Form
    {
        public WaveIn waveInStream ;
        public WaveFileWriter writer;
        public String timeStamp;
        public String senderEmail;
        public String receiverEmail;
        public String fileSavePath = "AudioFileSaved/";
        public Form1()
        {
            InitializeComponent();
            System.IO.StreamReader file = new System.IO.StreamReader("config.txt");
            string line;
            string[] data;
            int flag = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] spearator = { "<", ">" };

                data = line.Split(spearator, 2, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in data)
                {
                    if (str == "interval")
                        flag = 1;
                    else if (str == "recording_time")
                        flag = 2;
                    else if (str == "sender_email")
                        flag = 3;
                    else if (str == "receiver_email")
                        flag = 4;
                    else if (str == "path")
                        flag = 5;
                    else
                    {
                        switch (flag)
                        {
                            case 1:
                                timer1.Interval = Int32.Parse(str) * 1000;
                                break;
                            case 2:
                                timer2.Interval = Int32.Parse(str) * 1000;
                                break;
                            case 3:
                                senderEmail = str;
                                break;
                            case 4:
                                receiverEmail = str;
                                break;
                            case 5:
                                fileSavePath = str;
                                break;
                        }
                    }
                }
            }
            file.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();       
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            this.Visible = false;
            timer1.Enabled = false;
            timer2.Enabled = false;
            System.IO.StreamReader file = new System.IO.StreamReader("config.txt");
            string line;
            string[] data;
            int flag = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] spearator = { "<", ">" };

                data = line.Split(spearator, 2, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in data)
                {
                    if (str == "interval")
                        flag = 1;
                    else if (str == "recording_time")
                        flag = 2;
                    else if (str == "sender_email")
                        flag = 3;
                    else if (str == "receiver_email")
                        flag = 4;
                    else
                    {
                        switch (flag)
                        {
                            case 1:
                                timer1.Interval = Int32.Parse(str) * 1000;
                                break;
                            case 2:
                                timer2.Interval = Int32.Parse(str) * 1000;
                                break;
                            case 3:
                                senderEmail = str;
                                break;
                            case 4:
                                receiverEmail = str;
                                break;
                        }
                    }
                }
            }
            file.Close();
            timer1.Enabled = true;
        }

        private void OnMainTimer(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer("notification.wav");
            player.Play();
            if (!Directory.Exists(@fileSavePath))
                Directory.CreateDirectory(@fileSavePath);
            timeStamp = @fileSavePath + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".wav";
            waveInStream = new WaveIn();
            writer = new WaveFileWriter(timeStamp, waveInStream.WaveFormat);
            waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
            waveInStream.StartRecording();
            timer2.Enabled = true;
        }

        private void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
        }

        private void OnStopRecording(object sender, EventArgs e)
        {
            waveInStream.StopRecording();
            waveInStream.Dispose();
            waveInStream = null;
            writer.Close();
            writer = null;
            timer2.Enabled = false;
  /*          SmtpClient smtpClient = new SmtpClient();
            NetworkCredential smtpCredentials = new NetworkCredential("support@meuagente.com.br", "n(e{WIV%2?.g");

            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("support@meuagente.com.br");
            MailAddress toAddress = new MailAddress("bedditmail@gmail.com");
            MailAddress toAddress1 = new MailAddress("cechellafreelance@gmail.com");

            smtpClient.Host = "mail.meuagente.com.br";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = smtpCredentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            message.From = fromAddress;
            message.To.Add(toAddress);
            message.To.Add(toAddress1);
            message.IsBodyHtml = false;
            message.Subject = "Voice Message";
            message.Body = "A voice message was arrived!";
            message.Attachments.Add(new Attachment(timeStamp));
            smtpClient.Send(message); */
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            System.IO.StreamReader file = new System.IO.StreamReader("config.txt");
            string line;
            string[] data;
            int flag = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] spearator = { "<", ">" };

                data = line.Split(spearator, 2, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in data)
                {
                    if (str == "interval")
                        flag = 1;
                    else if (str == "recording_time")
                        flag = 2;
                    else if (str == "sender_email")
                        flag = 3;
                    else if (str == "receiver_email")
                        flag = 4;
                    else if (str == "path")
                        flag = 5;
                    else
                    {
                        switch (flag)
                        {
                            case 1:
                                timer1.Interval = Int32.Parse(str) * 1000;
                                break;
                            case 2:
                                timer2.Interval = Int32.Parse(str) * 1000;
                                break;
                            case 3:
                                senderEmail = str;
                                break;
                            case 4:
                                receiverEmail = str;
                                break;
                            case 5:
                                fileSavePath = str;
                                break;
                        }
                    }
                }
            }
            file.Close();
            timer1.Enabled = true;
        }
    }
}
