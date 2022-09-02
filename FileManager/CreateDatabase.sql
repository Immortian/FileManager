CREATE TABLE IF NOT EXISTS public."History"
(
    Id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    Filename character varying(30) COLLATE pg_catalog."default" NOT NULL,
    Date_visited timestamp without time zone NOT NULL,
    CONSTRAINT "History_pkey" PRIMARY KEY (Id)
);