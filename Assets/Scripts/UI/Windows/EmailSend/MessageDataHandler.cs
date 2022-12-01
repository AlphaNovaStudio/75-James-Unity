using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using UI.Suites;

namespace UI.Windows.EmailSend {
  public static class MessageDataHandler {
    public static MailMessage GetMessage (string sender, string addressee) {
      var message = new MailMessage {
        Body = GetBody(),
        Subject = "test mail",
        From = new MailAddress(sender)
      };

      message.To.Add(addressee);
      message.BodyEncoding = Encoding.UTF8;
      return message;
    }

    private static string GetBody() {
      List<SuiteCard> cards = FavoriteCardKeeper.FavoriteCards;
      var body = "";

      foreach (SuiteCard suiteCard in cards) {
        SuiteData data = suiteCard.GetData;
        body += GetCardInfo(data);
      }

      return body;
    }

    private static string GetCardInfo (SuiteData data) {
      string priceName = "Price";
      string numberName = "Number";
      var info = $"{data.SubTitle}\n{priceName} - {data.Price}$\n{numberName} - {data.SuiteNumber}\n";

      foreach (SuitePropertyData dataSuitePropertyData in data.SuitePropertyDatas) {
        info += $"{dataSuitePropertyData.PropertyName} - {dataSuitePropertyData.Count}\n";
      }

      return $"{info}\n";
    }
  }
}