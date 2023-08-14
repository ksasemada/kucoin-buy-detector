using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace kucoin_buy_detector
{
    public partial class Form1 : Form
    {
        double filtr_24 = 5000000;

        token_info token_info;

        string token_kucoin = "";
        string endpoint_kucoin = "";

        HttpClient client = new HttpClient();
        kucoin_symbols kucoin_symbols;
        kkucoin_allTickers kkucoin_allTickers;
        string all_token_usdt0 = "";
        string all_token_usdt1 = "";
        string all_token_usdt2 = "";
        string all_token_usdt3 = "";
        string all_token_usdt4 = "";
        string all_token_usdt5 = "";
        string all_token_usdt6 = "";
        string all_token_usdt7 = "";
        string all_token_usdt8 = "";
        string all_token_usdt9 = "";

        WebSocketSharp.WebSocket[] ws_a = new WebSocketSharp.WebSocket[20];

        string[] orden_last = new string[100];

        string[,] takerOrderId = new string[500000, 10];
        Int64 takerOrderId_kol = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Start." + Environment.NewLine, Color.Black)));

            await load_kucoin_symbols();
        }

        private async Task load_kucoin_symbols()
        {
            log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Load token..." + Environment.NewLine, Color.Black)));
            try
            {
                var time_vyp = DateTime.Now;
                // Create Request
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri("https://api.kucoin.com/api/v1/symbols");

                // Send Request
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    kucoin_symbols =
                        JsonConvert.DeserializeObject<kucoin_symbols>(await response.Content.ReadAsStringAsync());
                    var kol = 0;
                    foreach (var list in kucoin_symbols.data)
                    {
                        if (list.isMarginEnabled)
                        {
                            list.symbol = "isMarginEnabled";
                        }
                        if ((list.symbol.ToString().IndexOf("-") != -1) & (list.symbol.ToString().IndexOf("3S") == -1) & (list.symbol.ToString().IndexOf("3L") == -1) & (list.symbol.ToString().IndexOf("-USDT") != -1))
                        {
                            kol++;
                        }
                    }
                }
                await load_kucoin_allTickers();
            }
            catch (Exception ex2)
            {
                log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Error " + ex2.Message + Environment.NewLine, Color.DarkRed)));
            }
        }

        private async Task load_kucoin_allTickers() // cancellationToken- отмена задач
        {
            try
            {
                var time_vyp = DateTime.Now;
                // Create Request
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri("https://api.kucoin.com/api/v1/market/allTickers");

                // Send Request
                using (var clientt = new HttpClient())
                {
                    var response = await clientt.SendAsync(request);
                    //Console.WriteLine(response);
                    if (response.IsSuccessStatusCode)
                    {
                        var otv = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("otv:" + otv);
                        kkucoin_allTickers =
                            JsonConvert.DeserializeObject<kkucoin_allTickers>(otv);
                        var kol = 0;
                        foreach (var list in kkucoin_allTickers.data.ticker)
                        {
                            foreach (var list_kkucoin_allmoney in kucoin_symbols.data)
                            {
                                if (list_kkucoin_allmoney.symbol != null)
                                    if (list_kkucoin_allmoney.symbol.ToString() ==
                                            list.symbol.ToString())
                                    {
                                        //Console.WriteLine(list.symbol);
                                        if (list.volValue != null)
                                        {
                                            if ((list.symbol.ToString().IndexOf("-") != -1) & (list.symbol.ToString().IndexOf("3S") == -1) & (list.symbol.ToString().IndexOf("3L") == -1) & (list.symbol.ToString().IndexOf("-USDT") != -1) & (Convert.ToDouble(list.volValue.ToString().Replace(".", ",")) < filtr_24))
                                            {
                                                if ((kol >= 0) & (kol < 70))
                                                {
                                                    all_token_usdt0 = all_token_usdt0 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 70) & (kol < 140))
                                                {
                                                    all_token_usdt1 = all_token_usdt1 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 140) & (kol < 210))
                                                {
                                                    all_token_usdt2 = all_token_usdt2 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 210) & (kol < 280))
                                                {
                                                    all_token_usdt3 = all_token_usdt3 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 280) & (kol < 350))
                                                {
                                                    all_token_usdt4 = all_token_usdt4 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 350) & (kol < 420))
                                                {
                                                    all_token_usdt5 = all_token_usdt5 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 420) & (kol < 490))
                                                {
                                                    all_token_usdt6 = all_token_usdt6 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 490) & (kol < 560))
                                                {
                                                    all_token_usdt7 = all_token_usdt7 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 560) & (kol < 630))
                                                {
                                                    all_token_usdt8 = all_token_usdt8 + list.symbol.ToString() + ",";
                                                }
                                                if ((kol >= 630) & (kol < 700))
                                                {
                                                    all_token_usdt9 = all_token_usdt9 + list.symbol.ToString() + ",";
                                                }
                                                kol++;
                                            }
                                        }
                                    }
                            }
                        }
                        if (all_token_usdt0.Length > 0) { all_token_usdt0 = all_token_usdt0.Substring(0, all_token_usdt0.Length - 1); init_ws(0, all_token_usdt0); }
                        if (all_token_usdt1.Length > 0) { all_token_usdt1 = all_token_usdt1.Substring(0, all_token_usdt1.Length - 1); init_ws(1, all_token_usdt1); }
                        if (all_token_usdt2.Length > 0) { all_token_usdt2 = all_token_usdt2.Substring(0, all_token_usdt2.Length - 1); init_ws(2, all_token_usdt2); }
                        if (all_token_usdt3.Length > 0) { all_token_usdt3 = all_token_usdt3.Substring(0, all_token_usdt3.Length - 1); init_ws(3, all_token_usdt3); }
                        if (all_token_usdt4.Length > 0) { all_token_usdt4 = all_token_usdt4.Substring(0, all_token_usdt4.Length - 1); init_ws(4, all_token_usdt4); }
                        if (all_token_usdt5.Length > 0) { all_token_usdt5 = all_token_usdt5.Substring(0, all_token_usdt5.Length - 1); init_ws(5, all_token_usdt5); }
                        if (all_token_usdt6.Length > 0) { all_token_usdt6 = all_token_usdt6.Substring(0, all_token_usdt6.Length - 1); init_ws(6, all_token_usdt6); }
                        if (all_token_usdt7.Length > 0) { all_token_usdt7 = all_token_usdt7.Substring(0, all_token_usdt7.Length - 1); init_ws(7, all_token_usdt7); }
                        if (all_token_usdt8.Length > 0) { all_token_usdt8 = all_token_usdt8.Substring(0, all_token_usdt8.Length - 1); init_ws(8, all_token_usdt8); }
                        if (all_token_usdt9.Length > 0) { all_token_usdt9 = all_token_usdt9.Substring(0, all_token_usdt9.Length - 1); init_ws(9, all_token_usdt9); }
                        timer_ping.Enabled = true;
                        log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Load " + kol +
                            " token." + Environment.NewLine, Color.Black)));
                    }
                }
            }
            catch (Exception ex2)
            {
                log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Error " + ex2.Message + Environment.NewLine, Color.DarkRed)));
            }
        }

        async void init_ws(int num, string str)
        {
            try
            {
                var unixTime = Convert.ToDouble(new DateTimeOffset(DateTime.Now.ToUniversalTime()).ToUnixTimeMilliseconds());
                unixTime = unixTime + num;
                var request =
                    new HttpRequestMessage(HttpMethod.Post,
                        "https://api.kucoin.com" + "/api/v1/bullet-public");
                using (var clientt = new HttpClient())
                {
                    var response = await clientt.SendAsync(request);
                    var Result = await response.Content.ReadAsStringAsync();
                    token_info = JsonConvert.DeserializeObject<token_info>(Result);
                    token_kucoin = token_info.data.token;
                    endpoint_kucoin = token_info.data.instanceServers[0].endpoint;
                };
                ws_a[num] = new WebSocketSharp.WebSocket(endpoint_kucoin + "?token=" + token_kucoin + "&[connectId=" + unixTime + "]");

                ws_a[num].Log.Level = LogLevel.Error;
                ws_a[num].Origin = "https://www.kucoin.com";
                ws_a[num].SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

                ws_a[num].OnMessage += (sender2, e2) =>
                {
                    var strr = e2.Data;
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " (" + num + ") " + e2.Data);

                    if (strr.IndexOf("/market/match") != -1)
                    {
                        obr_match(strr, num);
                    }
                };
                ws_a[num].OnOpen += (sender2, e2) =>
                {
                    log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Open WS " + num + Environment.NewLine, Color.Black)));

                };

                ws_a[num].OnError += (sender2, e2) =>
                {
                    log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Error WS " + num + " " + e2.Message.ToString() + Environment.NewLine, Color.Red)));
                };

                ws_a[num].OnClose += async (sender2, e2) =>
                {
                    log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Close WS " + num + " " + e2.Code.ToString() + Environment.NewLine, Color.Red)));
                    await Task.Delay(1000);
                    init_ws(num, str);
                };

                ws_a[num].Connect();

                if (str.Length > 0) ws_a[num].Send("{\"id\":" + unixTime + ",\"type\":\"subscribe\",\"topic\":\"/market/match:" + str + "" + "\",\"privateChannel\":false,\"response\":true}");
            }
            catch (Exception ex2)
            {
                log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Error " + ex2.Message + Environment.NewLine, Color.DarkRed)));
            }

        }

        void obr_match(string strr, int num)
        {
            try
            {
                message_market_match message_market_match = JsonConvert.DeserializeObject<message_market_match>(strr);
                Int64 order = 0;
                if (message_market_match.data.side == "buy")
                {
                    for (Int64 a = 0; a < takerOrderId_kol; a++)
                    {
                        if (takerOrderId[a, 0] == message_market_match.data.takerOrderId.ToString())
                        {
                            takerOrderId[a, 1] = (Convert.ToInt64(takerOrderId[a, 1].Replace(".", ",")) + 1).ToString();
                            takerOrderId[a, 2] = (Convert.ToDouble(takerOrderId[a, 2].Replace(".", ",")) + Convert.ToDouble(message_market_match.data.price.Replace(".", ",")) * Convert.ToDouble(message_market_match.data.size.Replace(".", ","))).ToString();
                            takerOrderId[a, 6] = message_market_match.data.price.ToString();
                            order = a;
                            goto exit;
                        }
                    }
                    takerOrderId[takerOrderId_kol, 0] = message_market_match.data.takerOrderId.ToString();
                    takerOrderId[takerOrderId_kol, 1] = "1";
                    takerOrderId[takerOrderId_kol, 2] = (Convert.ToDouble(message_market_match.data.price.Replace(".", ",")) * Convert.ToDouble(message_market_match.data.size.Replace(".", ","))).ToString();
                    takerOrderId[takerOrderId_kol, 3] = "-";
                    takerOrderId[takerOrderId_kol, 4] = message_market_match.data.symbol.ToString();
                    takerOrderId[takerOrderId_kol, 5] = message_market_match.data.price.ToString();
                    takerOrderId[takerOrderId_kol, 6] = message_market_match.data.price.ToString();
                    order = takerOrderId_kol;
                    takerOrderId_kol++;
                exit:
                    kol_pos_label.Invoke(new Action(() => kol_pos_label.Text = "Parcels: " + takerOrderId_kol));
                }

                double min_ = 300; // filter

                if (orden_last[num] != message_market_match.data.takerOrderId)// new
                {
                    string outt = "";

                        long kol_ = 0;
                        double sum_usdt = 0;
                        string symbol = "";
                        string proc_f_n = "0";
                        string proc_s_n = "0";

                    for (Int64 a = 0; a < takerOrderId_kol; a++)
                    {
                        if (takerOrderId[a, 0] == orden_last[num])
                        {
                            kol_ = Convert.ToInt64(takerOrderId[a, 1].Replace(".", ","));
                            sum_usdt = Convert.ToDouble(takerOrderId[a, 2].Replace(".", ","));
                            symbol = takerOrderId[a, 4];
                            proc_f_n = (((Convert.ToDouble(takerOrderId[a, 6].Replace(".", ",")) - Convert.ToDouble(takerOrderId[a, 5].Replace(".", ","))) / Convert.ToDouble(takerOrderId[a, 6].Replace(".", ","))) * 100).ToString("N0");
                            proc_s_n = takerOrderId[a, 6];
                            goto exit;
                        }
                    }
                    exit:

                        if (sum_usdt > min_)
                        {
                            bool en_out = true;

                            string dop_ipfo_001 = "U";
                            var vol24 = "0";
                            foreach (var list in kkucoin_allTickers.data.ticker)
                            {
                                if (symbol == list.symbol.ToString())
                                {
                                    vol24 = (Convert.ToDouble(list.volValue.ToString().Replace(".", ","))).ToString("F0");
                                    proc_s_n = (((Convert.ToDouble(proc_s_n.Replace(".", ",")) - Convert.ToDouble(list.last.Replace(".", ","))) / Convert.ToDouble(proc_s_n.Replace(".", ","))) * 100).ToString("N0");
                                }
                                var _ETH = symbol.Replace("USDT", "ETH");
                                if (_ETH == list.symbol.ToString())
                                {
                                    dop_ipfo_001 = dop_ipfo_001 + "E";
                                }
                                var _BTC = symbol.Replace("USDT", "BTC");
                                if (_BTC == list.symbol.ToString())
                                {
                                    dop_ipfo_001 = dop_ipfo_001 + "B";
                                }

                            }

                            outt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "(" + num + ") " + symbol + " " +
                            sum_usdt.ToString("F0") +
                            " USDT purchase. Changing " +
                            proc_f_n + "%." +
                            "In order:" + kol_ + "." +
                            " Volume 24h=" + vol24 + " USDT." +
                            " " + dop_ipfo_001 + "."
                            ;

                            if (en_out)
                                ((RichTextBox)log_richTextBox).AppendText(
                                outt +
                                Environment.NewLine, Color.Black);

                        }
                    }
                    orden_last[num] = message_market_match.data.takerOrderId;
            }
            catch (Exception ex2)
            {
                log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Error " + ex2.Message + Environment.NewLine, Color.DarkRed)));
            }
        }

        private void timer_ping_Tick(object sender, EventArgs e)
        {
            try
            {
                var unixTime = Convert.ToDouble(new DateTimeOffset(DateTime.Now.ToUniversalTime()).ToUnixTimeMilliseconds());
                //ws[0].Send("{\"id\":\""+ unixTime + "\",\"type\":\"ping\"}");
                if (all_token_usdt0.Length > 0) if (ws_a[0].ReadyState.ToString() == "Open") { ws_a[0].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt1.Length > 0) if (ws_a[1].ReadyState.ToString() == "Open") { ws_a[1].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt2.Length > 0) if (ws_a[2].ReadyState.ToString() == "Open") { ws_a[2].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt3.Length > 0) if (ws_a[3].ReadyState.ToString() == "Open") { ws_a[3].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt4.Length > 0) if (ws_a[4].ReadyState.ToString() == "Open") { ws_a[4].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt5.Length > 0) if (ws_a[5].ReadyState.ToString() == "Open") { ws_a[5].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt6.Length > 0) if (ws_a[6].ReadyState.ToString() == "Open") { ws_a[6].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt7.Length > 0) if (ws_a[7].ReadyState.ToString() == "Open") { ws_a[7].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt8.Length > 0) if (ws_a[8].ReadyState.ToString() == "Open") { ws_a[8].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
                if (all_token_usdt9.Length > 0) if (ws_a[9].ReadyState.ToString() == "Open") { ws_a[9].Send("{\"id\":\"" + unixTime + "\",\"type\":\"ping\"}"); }
            }
            catch (Exception ex2)
            {
                log_richTextBox.Invoke(new Action(() => log_richTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " Error " + ex2.Message + Environment.NewLine, Color.DarkRed)));
            }
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            if (box.Visible || box.Name == "log_richTextBox")
            {
                box.Invoke(new Action(() => box.SelectionStart = box.TextLength));
                box.Invoke(new Action(() => box.SelectionLength = 0));
                box.Invoke(new Action(() => box.SelectionColor = color));
                box.Invoke(new Action(() => box.AppendText(text)));
                box.Invoke(new Action(() => box.SelectionColor = box.ForeColor));
            }
        }
    }

    public class Datum_kucoin_symbols
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string baseCurrency { get; set; }
        public string quoteCurrency { get; set; }
        public string feeCurrency { get; set; }
        public string market { get; set; }
        public string baseMinSize { get; set; }
        public string quoteMinSize { get; set; }
        public string baseMaxSize { get; set; }
        public string quoteMaxSize { get; set; }
        public string baseIncrement { get; set; }
        public string quoteIncrement { get; set; }
        public string priceIncrement { get; set; }
        public string priceLimitRate { get; set; }
        public string minFunds { get; set; }
        public bool isMarginEnabled { get; set; }
        public bool enableTrading { get; set; }
    }

    public class kucoin_symbols
    {
        public string code { get; set; }
        public List<Datum_kucoin_symbols> data { get; set; }
    }

    public class Data_kkucoin_allTickers
    {
        public long time { get; set; }
        public List<Ticker_kkucoin_allTickers> ticker { get; set; }
    }

    public class kkucoin_allTickers
    {
        public string code { get; set; }
        public Data_kkucoin_allTickers data { get; set; }
    }

    public class Ticker_kkucoin_allTickers
    {
        public string symbol { get; set; }
        public string symbolName { get; set; }
        public string buy { get; set; }
        public string sell { get; set; }
        public string changeRate { get; set; }
        public string changePrice { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string vol { get; set; }
        public string volValue { get; set; }
        public string last { get; set; }
        public string averagePrice { get; set; }
        public string takerFeeRate { get; set; }
        public string makerFeeRate { get; set; }
        public string takerCoefficient { get; set; }
        public string makerCoefficient { get; set; }
    }

    public class token_info
    {
        public string code { get; set; }
        public Data_token_info data { get; set; }
    }

    public class Data_token_info
    {
        public string token { get; set; }
        public List<InstanceServer_token_info> instanceServers { get; set; }
    }

    public class InstanceServer_token_info
    {
        public string endpoint { get; set; }
        public bool encrypt { get; set; }
        public string protocol { get; set; }
        public int pingInterval { get; set; }
        public int pingTimeout { get; set; }
    }

    public class Data_message_market_match
    {
        public string makerOrderId { get; set; }
        public string price { get; set; }
        public string sequence { get; set; }
        public string side { get; set; }
        public string size { get; set; }
        public string symbol { get; set; }
        public string takerOrderId { get; set; }
        public string time { get; set; }
        public string tradeId { get; set; }
        public string type { get; set; }
    }

    public class message_market_match
    {
        public string type { get; set; }
        public string topic { get; set; }
        public string subject { get; set; }
        public Data_message_market_match data { get; set; }
    }
}
