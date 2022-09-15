
INSERT INTO "Employee" ("Guid","FirstName","LastName","Birthdate","Phone","Email","City","Role") VALUES 
 ('3de7139f-61fd-466d-a434-b83dd836c85e','Harrison','Collick','1993-06-24','851212321321','collick@company.com','Gothenburg','Admin'),
 ('a3a3e1a9-1efa-4b36-8c0c-124016c946cd','Willabella','Juan','1980/03/20','7414144423','wjuan1@apple.com','Drochia','Admin'),
 ('aa870a58-152c-47f6-b5d7-fb80b6f30a53','Agretha','Ding','1979/03/21','6137414720','ading2@illinois.edu','Satinka','Manager'),
 ('52698b2d-3bdf-4cd1-a2db-c3f1e538abf7','Marve','Dalligan','1983/11/22','3738851099','mdalligan3@rediff.com','Schaan','Manager, Admin, Employee'),
 ('7f483baa-0432-4f24-b3f7-22cf41f2d21c','Dorelle','Gutteridge','1981/11/22','6125078546','dgutteridge4@si.edu','Primorsko-Akhtarsk','Manager'),
 ('cccc1337-f141-4e50-988f-313bcc27b62b','Tito','Christou','1993/05/11','1777147516','tchristou5@microsoft.com','Fort Dauphin','Employee'),
 ('c1c45260-c47c-4d29-af3c-5a7b5a43a003','Beulah','Casterton','1980/03/20','9118423574','bcasterton6@mapquest.com','Ipatinga','Employee'),
 ('f523a304-9aba-45bd-9de1-398041bebcbb','Marven','Huyge','1993/05/11','2463956603','mhuyge7@cafepress.com','Přibyslav','Employee'),
 ('268814ac-40b9-43d7-8ed5-f935a6aaeace','Junie','Ellesworthe','1994/08/01','7286012979','jellesworthe8@taobao.com','Songnan','Admin'),
 ('cb17d212-aca4-4201-9ff2-9e9f5dfd10f8','Roselle','Franchi','1988/07/03','8349750994','rfranchi9@prweb.com','Stare Kurowo','Employee'),
 ('52905ee4-b8a6-402b-a279-5b00378d0f71','Jeffy','Hawkey','1980/03/20','3168901530','jhawkeya@uol.com.br','Pangrumasan','Employee'),
 ('50dd6bb3-6ec2-43c0-b822-2466a183be8b','Alex','Craigie','1988/07/03','9318954417','acraigieb@examiner.com','Renfrew','Admin');

Update Employee set [Guid] = Upper([Guid]); 

