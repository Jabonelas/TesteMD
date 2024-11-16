-- Table: public.tb_clientes

-- DROP TABLE IF EXISTS public.tb_clientes;

CREATE TABLE IF NOT EXISTS public.tb_clientes
(
    id_cliente integer NOT NULL DEFAULT nextval('clientes_id_cliente_seq'::regclass),
    cl_nome character varying(200) COLLATE pg_catalog."default" NOT NULL,
    cl_endereco character varying(200) COLLATE pg_catalog."default" NOT NULL,
    cl_telefone character varying(30) COLLATE pg_catalog."default" NOT NULL,
    cl_email character varying(250) COLLATE pg_catalog."default" NOT NULL,
    cl_ativo boolean NOT NULL,
    cl_cpf character varying(14) COLLATE pg_catalog."default" NOT NULL,
    cl_cep character varying(9) COLLATE pg_catalog."default" NOT NULL,
    cl_bairro character varying(100) COLLATE pg_catalog."default" NOT NULL,
    cl_cidade character varying(100) COLLATE pg_catalog."default" NOT NULL,
    cl_estado character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT clientes_pkey PRIMARY KEY (id_cliente)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.tb_clientes
    OWNER to postgres;