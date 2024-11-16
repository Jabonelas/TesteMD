-- Table: public.tb_vendas

-- DROP TABLE IF EXISTS public.tb_vendas;

CREATE TABLE IF NOT EXISTS public.tb_vendas
(
    id_venda integer NOT NULL DEFAULT nextval('tb_vendas_id_venda_seq'::regclass),
    fk_tb_cliente integer,
    fk_tb_produto integer,
    ve_quantidade integer,
    CONSTRAINT venda_pkey PRIMARY KEY (id_venda),
    CONSTRAINT fk_clientes FOREIGN KEY (fk_tb_cliente)
        REFERENCES public.tb_clientes (id_cliente) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT fk_tb_produtos FOREIGN KEY (fk_tb_produto)
        REFERENCES public.tb_produtos (id_produto) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.tb_vendas
    OWNER to postgres;