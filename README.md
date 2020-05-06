# Cookbook

Repozitorij koji je razvijen za potrebe pisanja diplomskog rada *Razvoj web-aplikacija baziran na testiranju*. Unutar repozitorija razvijena su tri projekta: *Cookbook.HighLevelTests*, *Cookbook.LowerLevelTests* i *Cookbook*. Unutar projekta *Cookbook*, pomoću metode **Test-Driven Development (TDD)**, razvijene su neke funkcionalnosti aplikacije te su u potpunosti pokrivene testovima. Funkcionalnosti koje obuhvaća aplikacija jesu: pregled svih recepata te pojedinačnog recepta i dodavanje novog recepta. 
Aplikacija s dodatnim funkcionalnostima nalazi se unutar repozitorija [*Kuharica*](https://github.com/petrarozic/Kuharica).

Okvir unutar kojega je razvijena aplikacija jest **ASP.NET Core 2.2**. Za bazu je korištena lokalna baza **MSSQL**.

Testovi su podijeljeni u dvije kategorije:
-	Testovi visoke razine (eng. *high level tests*) - testiraju funkcionalnosti aplikacije sa strane korisnika (razvijeni unutar projekta Cookbook.HighLevelTests)
-	Testovi niže razine (eng. *lower level tests*) - testiraju funkcionalnosti aplikacije sa strane programera (razvijeni unutar projekta Cookbook.LowerLevelTests)

Alati pomoću kojih su pisani testovi jesu: **xUnit**, **Moq**, **Selenium**. 
