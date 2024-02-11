using Flipster.Identity.Core.Domain.Entities;

namespace Flipster.Identity.Core.Data.Seeds;

public class LocationSeeder : IDataSeeder<Location>
{
    public const string PathToFile = "./LocationSeedData.json";
    public void Seed(ApplicationDbContext db)
    {
        db.AddRange(
            new Location("Ukraine"),
            new Location("Ukraine, Vinnytska Oblast"),
            new Location("Ukraine, Volynska Oblast"),
            new Location("Ukraine, Dnipropetrovska Oblast"),
            new Location("Ukraine, Donetska Oblast"),
            new Location("Ukraine, Zhytomyrska Oblast"),
            new Location("Ukraine, Zakarpatska Oblast"),
            new Location("Ukraine, Zaporizka Oblast"),
            new Location("Ukraine, Ivano-Frankivska Oblast"),
            new Location("Ukraine, Kyivska Oblast"),
            new Location("Ukraine, Kirovohradska Oblast"),
            new Location("Ukraine, Luhanska Oblast"),
            new Location("Ukraine, Lvivska Oblast"),
            new Location("Ukraine, Mykolaivska Oblast"),
            new Location("Ukraine, Odeska Oblast"),
            new Location("Ukraine, Poltavska Oblast"),
            new Location("Ukraine, Rivnenska Oblast"),
            new Location("Ukraine, Sumska Oblast"),
            new Location("Ukraine, Ternopilska Oblast"),
            new Location("Ukraine, Kharkivska Oblast"),
            new Location("Ukraine, Khersonska Oblast"),
            new Location("Ukraine, Khmelnytska Oblast"),
            new Location("Ukraine, Cherkaska Oblast"),
            new Location("Ukraine, Chernivetska Oblast"),
            new Location("Ukraine, Chernihivska Oblast"),
            new Location("Ukraine, Kyivska Oblast, Kyiv"),
            new Location("Ukraine, Kharkivska Oblast, Kharkiv"),
            new Location("Ukraine, Odeska Oblast, Odesa"),
            new Location("Ukraine, Dnipropetrovska Oblast, Dnipro"),
            new Location("Ukraine, Donetska Oblast, Donetsk"),
            new Location("Ukraine, Zaporizka Oblast, Zaporizhzhia"),
            new Location("Ukraine, Lvivska Oblast, Lviv"),
            new Location("Ukraine, Dnipropetrovska Oblast, Kryvyi Rih"),
            new Location("Ukraine, Sevastopolska Oblast, Sevastopol"),
            new Location("Ukraine, Mykolaivska Oblast, Mykolaiv"),
            new Location("Ukraine, Donetska Oblast, Mariupol"),
            new Location("Ukraine, Luhanska Oblast, Luhansk"),
            new Location("Ukraine, Vinnytska Oblast, Vinnytsia"),
            new Location("Ukraine, Donetska Oblast, Makiivka"),
            new Location("Ukraine, Krym, Avtonomna Respublika, Simferopol"),
            new Location("Ukraine, Poltavska Oblast, Poltava"),
            new Location("Ukraine, Chernihivska Oblast, Chernihiv"),
            new Location("Ukraine, Khersonska Oblast, Kherson"),
            new Location("Ukraine, Cherkaska Oblast, Cherkasy"),
            new Location("Ukraine, Khmelnytska Oblast, Khmelnytskyi"),
            new Location("Ukraine, Chernivetska Oblast, Chernivtsi"),
            new Location("Ukraine, Sumska Oblast, Sumy"),
            new Location("Ukraine, Zhytomyrska Oblast, Zhytomyr"),
            new Location("Ukraine, Donetska Oblast, Horlivka"),
            new Location("Ukraine, Rivnenska Oblast, Rivne"),
            new Location("Ukraine, Ivano-Frankivska Oblast, Ivano-Frankivsk"),
            new Location("Ukraine, Dnipropetrovska Oblast, Kamianske"),
            new Location("Ukraine, Kirovohradska Oblast, Kropyvnytskyi"),
            new Location("Ukraine, Ternopilska Oblast, Ternopil"),
            new Location("Ukraine, Poltavska Oblast, Kremenchuk"),
            new Location("Ukraine, Volynska Oblast, Lutsk"),
            new Location("Ukraine, Kyivska Oblast, Bila Tserkva"),
            new Location("Ukraine, Donetska Oblast, Kramatorsk"),
            new Location("Ukraine, Zaporizka Oblast, Melitopol"),
            new Location("Ukraine, Luhanska Oblast, Sievierodonetsk"),
            new Location("Ukraine, Krym, Avtonomna Respublika, Kerch"),
            new Location("Ukraine, Lvivska Oblast, Drohobych"),
            new Location("Ukraine, Luhanska Oblast, Khrustalnyi"),
            new Location("Ukraine, Zakarpatska Oblast, Uzhhorod"),
            new Location("Ukraine, Zaporizka Oblast, Berdiansk"),
            new Location("Ukraine, Donetska Oblast, Sloviansk"),
            new Location("Ukraine, Dnipropetrovska Oblast, Nikopol"),
            new Location("Ukraine, Kyivska Oblast, Brovary"),
            new Location("Ukraine, Krym Avtonomna Respublika, Yevpatoriia"),
            new Location("Ukraine, Luhanska Oblast, Lysychansk"),
            new Location("Ukraine, Cherkaska Oblast, Smila"),
            new Location("Ukraine, Zaporizka Oblast, Enerhodar"),
            new Location("Ukraine, Donetska Oblast, Stakhanov"),
            new Location("Ukraine, Khmelnytska Oblast, Kamianets-Podilskyi"),
            new Location("Ukraine, Donetska Oblast, Toretsk"),
            new Location("Ukraine, Zaporizka Oblast, Prymorsk"),
            new Location("Ukraine, Krym, Avtonomna Respublika, Feodosiia"),
            new Location("Ukraine, Kyivska Oblast, Irpin"),
            new Location("Ukraine, Khmelnytska Oblast, Netishyn"),
            new Location("Ukraine, Zhytomyrska Oblast, Berdychiv"),
            new Location("Ukraine, Donetska Oblast, Debaltseve"),
            new Location("Ukraine, Rivnenska Oblast, Kostopil"),
            new Location("Ukraine, Zaporizka Oblast, Tokmak"),
            new Location("Ukraine, Dnipropetrovska Oblast, Zhovti Vody"),
            new Location("Ukraine, Khmelnytska Oblast, Shepetivka"),
            new Location("Ukraine, Zhytomyrska Oblast, Malyn"),
            new Location("Ukraine, Rivnenska Oblast, Radyvyliv"),
            new Location("Ukraine, Donetska Oblast, Antratsyt"),
            new Location("Ukraine, Zakarpatska Oblast, Mukacheve"),
            new Location("Ukraine, Zhytomyrska Oblast, Korosten"),
            new Location("Ukraine, Rivnenska Oblast, Dubno"),
            new Location("Ukraine, Zaporizka Oblast, Vasylkivka"),
            new Location("Ukraine, Khmelnytska Oblast, Krasyliv"),
            new Location("Ukraine, Chernivetska Oblast, Novoselytsia"),
            new Location("Ukraine, Volynska Oblast, Novovolynsk"),
            new Location("Ukraine, Rivnenska Oblast, Varash"),
            new Location("Ukraine, Khmelnytska Oblast, Dunaivtsi"),
            new Location("Ukraine, Zaporizka Oblast, Vilniansk"),
            new Location("Ukraine, Zhytomyrska Oblast, Korostyshiv"),
            new Location("Ukraine, Chernivetska Oblast, Khotyn"),
            new Location("Ukraine, Rivnenska Oblast, Zdolbuniv"),
            new Location("Ukraine, Zaporizka Oblast, Velyka Novosilka"),
            new Location("Ukraine, Khmelnytska Oblast, Slavuta"),
            new Location("Ukraine, Zhytomyrska Oblast, Ovruch"),
            new Location("Ukraine, Rivnenska Oblast, Kuznetsove"),
            new Location("Ukraine, Zaporizka Oblast, Yakymivka"),
            new Location("Ukraine, Khmelnytska Oblast, Volochysk"),
            new Location("Ukraine, Zhytomyrska Oblast, Korostyshev"),
            new Location("Ukraine, Rivnenska Oblast, Kostopil"),
            new Location("Ukraine, Zaporizka Oblast, Tokmak"),
            new Location("Ukraine, Dnipropetrovska Oblast, Zhovti Vody"),
            new Location("Ukraine, Khmelnytska Oblast, Shepetivka"),
            new Location("Ukraine, Zhytomyrska Oblast, Malyn"),
            new Location("Ukraine, Rivnenska Oblast, Radyvyliv"),
            new Location("Ukraine, Donetska Oblast, Antratsyt"),
            new Location("Ukraine, Zakarpatska Oblast, Mukacheve"),
            new Location("Ukraine, Zhytomyrska Oblast, Korosten"),
            new Location("Ukraine, Rivnenska Oblast, Dubno"),
            new Location("Ukraine, Zaporizka Oblast, Vasylkivka"),
            new Location("Ukraine, Khmelnytska Oblast, Krasyliv"),
            new Location("Ukraine, Chernivetska Oblast, Novoselytsia"),
            new Location("Ukraine, Volynska Oblast, Novovolynsk"),
            new Location("Ukraine, Rivnenska Oblast, Varash"),
            new Location("Ukraine, Khmelnytska Oblast, Dunaivtsi"),
            new Location("Ukraine, Zaporizka Oblast, Vilniansk"),
            new Location("Ukraine, Zhytomyrska Oblast, Korostyshiv"),
            new Location("Ukraine, Chernivetska Oblast, Khotyn"),
            new Location("Ukraine, Rivnenska Oblast, Zdolbuniv"),
            new Location("Ukraine, Zaporizka Oblast, Velyka Novosilka"),
            new Location("Ukraine, Khmelnytska Oblast, Slavuta"),
            new Location("Ukraine, Zhytomyrska Oblast, Ovruch"),
            new Location("Ukraine, Rivnenska Oblast, Kuznetsove"),
            new Location("Ukraine, Zaporizka Oblast, Yakymivka"),
            new Location("Ukraine, Khmelnytska Oblast, Volochysk"),
            new Location("Ukraine, Zhytomyrska Oblast, Korostyshev")
        );
        db.SaveChanges();
    }
}