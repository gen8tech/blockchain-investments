
CREATE TABLE public.Book (
                AgregateId CHAR(36) NOT NULL,
                ExternalUserId CHAR(36) NOT NULL,
                DtCreate TIMESTAMP NOT NULL,
                DtModified TIMESTAMP NOT NULL,
                CONSTRAINT book_pk PRIMARY KEY (AgregateId)
);


CREATE TABLE public.PricingType (
                Id INTEGER NOT NULL,
                Description VARCHAR(50) NOT NULL,
                CONSTRAINT pricingtype_pk PRIMARY KEY (Id)
);


CREATE TABLE public.JournalType (
                Id INTEGER NOT NULL,
                Description VARCHAR(50) NOT NULL,
                CONSTRAINT journaltype_pk PRIMARY KEY (Id)
);


CREATE TABLE public.PricingMechanism (
                Id INTEGER NOT NULL,
                Description VARCHAR(50) NOT NULL,
                CONSTRAINT pricingmechanism_pk PRIMARY KEY (Id)
);


CREATE TABLE public.MarketType (
                Id INTEGER NOT NULL,
                Description VARCHAR(50) NOT NULL,
                CONSTRAINT markettype_pk PRIMARY KEY (Id)
);


CREATE SEQUENCE public.security_id_seq;

CREATE TABLE public.Security (
                Id BIGINT NOT NULL DEFAULT nextval('public.security_id_seq'),
                pm_id INTEGER NOT NULL,
                mt_id INTEGER NOT NULL,
                Title VARCHAR(100) NOT NULL,
                Ticker CHAR(10) NOT NULL,
                Code CHAR(10) NOT NULL,
                Fraction BIGINT NOT NULL,
                ImageUrl VARCHAR(500) NOT NULL,
                DetailsUrl VARCHAR(500) NOT NULL,
                BlockExplorerUrl VARCHAR(500) NOT NULL,
                Description VARCHAR(200) NOT NULL,
                Namespace VARCHAR(200) NOT NULL,
                QuoteSource VARCHAR(500) NOT NULL,
                CONSTRAINT security_pk PRIMARY KEY (Id)
);


ALTER SEQUENCE public.security_id_seq OWNED BY public.Security.Id;

CREATE SEQUENCE public.price_id_seq;

CREATE TABLE public.Price (
                Id BIGINT NOT NULL DEFAULT nextval('public.price_id_seq'),
                prt_id INTEGER NOT NULL,
                cur_id BIGINT NOT NULL,
                sec_id BIGINT NOT NULL,
                DtCreated TIMESTAMP NOT NULL,
                Source VARCHAR(500) NOT NULL,
                Value DOUBLE PRECISION NOT NULL,
                CONSTRAINT price_pk PRIMARY KEY (Id)
);


ALTER SEQUENCE public.price_id_seq OWNED BY public.Price.Id;

CREATE TABLE public.CounterpartyType (
                Id INTEGER NOT NULL,
                Description VARCHAR(50) NOT NULL,
                CONSTRAINT counterpartytype_pk PRIMARY KEY (Id)
);


CREATE TABLE public.AccountTupe (
                Id INTEGER NOT NULL,
                Description VARCHAR(50) NOT NULL,
                CONSTRAINT accounttupe_pk PRIMARY KEY (Id)
);


CREATE SEQUENCE public.account_id_seq;

CREATE TABLE public.Account (
                Id BIGINT NOT NULL DEFAULT nextval('public.account_id_seq'),
                AgregateId CHAR(36) NOT NULL,
                Title VARCHAR(100) NOT NULL,
                Description VARCHAR(200) NOT NULL,
                Notes VARCHAR(8000) NOT NULL,
                Code VARCHAR(200) NOT NULL,
                act_id INTEGER NOT NULL,
                sec_id BIGINT NOT NULL,
                cpt_id INTEGER NOT NULL,
                ParentAccountId BIGINT NOT NULL,
                DtCreated TIMESTAMP NOT NULL,
                CONSTRAINT account_pk PRIMARY KEY (Id)
);


ALTER SEQUENCE public.account_id_seq OWNED BY public.Account.Id;

CREATE SEQUENCE public.transaction_id_seq;

CREATE TABLE public.Transaction (
                Id BIGINT NOT NULL DEFAULT nextval('public.transaction_id_seq'),
                jor_id INTEGER NOT NULL,
                prc_id BIGINT NOT NULL,
                acc_id BIGINT NOT NULL,
                Tag VARCHAR(50) NOT NULL,
                ExternalTransactionId VARCHAR(500) NOT NULL,
                DtCreated TIMESTAMP NOT NULL,
                CONSTRAINT transaction_pk PRIMARY KEY (Id)
);


ALTER SEQUENCE public.transaction_id_seq OWNED BY public.Transaction.Id;

CREATE SEQUENCE public.journalentry_id_seq;

CREATE TABLE public.JournalEntry (
                Id BIGINT NOT NULL DEFAULT nextval('public.journalentry_id_seq'),
                sec_id BIGINT NOT NULL,
                EventDate TIMESTAMP NOT NULL,
                Description VARCHAR(200) NOT NULL,
                IdFrom BIGINT NOT NULL,
                IdTo BIGINT NOT NULL,
                DtCreated TIMESTAMP NOT NULL,
                CONSTRAINT journalentry_pk PRIMARY KEY (Id)
);


ALTER SEQUENCE public.journalentry_id_seq OWNED BY public.JournalEntry.Id;

ALTER TABLE public.Account ADD CONSTRAINT book_account_fk
FOREIGN KEY (AgregateId)
REFERENCES public.Book (AgregateId)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Price ADD CONSTRAINT pricingtype_price_fk
FOREIGN KEY (prt_id)
REFERENCES public.PricingType (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Transaction ADD CONSTRAINT journaltype_transaction_fk
FOREIGN KEY (jor_id)
REFERENCES public.JournalType (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Security ADD CONSTRAINT pricingmechanism_security_fk
FOREIGN KEY (pm_id)
REFERENCES public.PricingMechanism (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Security ADD CONSTRAINT markettype_security_fk
FOREIGN KEY (mt_id)
REFERENCES public.MarketType (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Account ADD CONSTRAINT security_account_fk
FOREIGN KEY (sec_id)
REFERENCES public.Security (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Price ADD CONSTRAINT security_price_fk
FOREIGN KEY (sec_id)
REFERENCES public.Security (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Price ADD CONSTRAINT security_price_fk1
FOREIGN KEY (cur_id)
REFERENCES public.Security (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.JournalEntry ADD CONSTRAINT security_journalentry_fk
FOREIGN KEY (sec_id)
REFERENCES public.Security (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Transaction ADD CONSTRAINT price_transaction_fk
FOREIGN KEY (prc_id)
REFERENCES public.Price (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Account ADD CONSTRAINT counterpartytype_account_fk
FOREIGN KEY (cpt_id)
REFERENCES public.CounterpartyType (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Account ADD CONSTRAINT accounttupe_account_fk
FOREIGN KEY (act_id)
REFERENCES public.AccountTupe (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Transaction ADD CONSTRAINT account_transaction_fk
FOREIGN KEY (acc_id)
REFERENCES public.Account (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.JournalEntry ADD CONSTRAINT transaction_journalentry_fk
FOREIGN KEY (IdFrom)
REFERENCES public.Transaction (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.JournalEntry ADD CONSTRAINT transaction_journalentry_fk1
FOREIGN KEY (IdTo)
REFERENCES public.Transaction (Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;