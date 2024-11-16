-- Table: public.tb_produtos

-- DROP TABLE IF EXISTS public.tb_produtos;

CREATE TABLE IF NOT EXISTS public.tb_produtos
(
    id_produto integer NOT NULL DEFAULT nextval('tb_produtos_id_produto_seq'::regclass),
    pd_nome character varying(200) COLLATE pg_catalog."default" NOT NULL,
    pd_descricao character varying(200) COLLATE pg_catalog."default" NOT NULL,
    pd_preco numeric(10,2) NOT NULL,
    pd_ativo boolean NOT NULL,
    pd_estoque integer,
    CONSTRAINT produtos_pkey PRIMARY KEY (id_produto)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.tb_produtos
    OWNER to postgres;