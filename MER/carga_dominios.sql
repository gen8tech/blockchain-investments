--ATENÇÃO RODAR ESSE SCRIPT UMA VEZ SÓ!!!

--select * from accounttype
insert into accounttype (description) values ('Asset');
insert into accounttype (description) values ('Liability');
insert into accounttype (description) values ('Equity');
insert into accounttype (description) values ('Revenue');
insert into accounttype (description) values ('Expense');

--select * from counterpartytype
insert into counterpartytype (description) values ('OwnProperty');
insert into counterpartytype (description) values ('Bank');
insert into counterpartytype (description) values ('Brokerage');
insert into counterpartytype (description) values ('OtherService');

--select * from markettype

insert into markettype (description) values ('ForexCurrency');
insert into markettype (description) values ('CryptoCurrency');
insert into markettype (description) values ('Metal');
insert into markettype (description) values ('Stock');
insert into markettype (description) values ('Bond');
insert into markettype (description) values ('ETF');
insert into markettype (description) values ('Commodity');
insert into markettype (description) values ('Indice');

--select * from pricingmechanism

insert into pricingmechanism (description) values ('Historical');
insert into pricingmechanism (description) values ('Market');

--select * from pricingtype

insert into pricingtype (description) values ('Bid');
insert into pricingtype (description) values ('Ask');
insert into pricingtype (description) values ('Last');
insert into pricingtype (description) values ('NetAssetValue');

--select * from journaltype

insert into journaltype (description) values ('Debit');
insert into journaltype (description) values ('Credit');

