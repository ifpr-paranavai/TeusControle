create database teuscontroledb;
use teuscontroledb;

-- drop database teuscontroledb;


select * from products_sale; 
select * from sales;
select * from products_entry;
select * from products;
select * from entries;

drop table sales;

update sales set CpfCnpj = null where id > 1;
update entries set deleted = 1 where id = 56;
update products set deleted = 0 where id > 0;

-- drop table entry;
-- drop table productentry;
-- drop database teuscontroledb;
-- UPDATE `teuscontroledb`.`users` SET `deleted` = 0 where `Id` > 1;

drop trigger calculate_total_entry_value_on_update;
drop trigger calculate_total_entry_value_on_insert;

DELIMITER $$
CREATE TRIGGER calculate_total_entry_value_on_insert 
AFTER INSERT ON products_entry
FOR EACH ROW
  begin
	DECLARE total_entry_value DECIMAL(65,30);
	
    SELECT SUM(pe.TotalPrice) 
    INTO @total_entry_value 
    FROM teuscontroledb.products_entry pe
    INNER JOIN teuscontroledb.entries e
    ON pe.Id = e.Id
    WHERE pe.Id = NEW.Id;
  
	UPDATE entries
    SET	TotalPrice = @total_entry_value
    WHERE Id = NEW.Id;
  end;
$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER calculate_total_entry_value_on_update 
AFTER UPDATE ON products_entry
FOR EACH ROW
  begin
	DECLARE total_entry_value DECIMAL(65,30);
	
    SELECT SUM(pe.TotalPrice) 
    INTO @total_entry_value 
    FROM teuscontroledb.products_entry pe
    INNER JOIN teuscontroledb.entries e
    ON pe.Id = e.Id
    WHERE pe.Id = NEW.Id;
  
	UPDATE entries
    SET	TotalPrice = @total_entry_value
    WHERE Id = NEW.Id;
  end;
$$
DELIMITER ;



-- minhasenha
-- AQAAAAEAACcQAAAAENyM1cl0om7hIZ9ofhdGovH3rugpKTMj/hF9N5TG2QZb6N57CsUhbBtTWLkKIQqP/Q==

-- alter table entry drop column TotalPrice;
-- alter table entry add column TotalPrice decimal(65,30) default 0;

-- drop trigger calculate_total_sale_value_on_insert;

DELIMITER $$
CREATE TRIGGER calculate_total_sale_value_on_insert 
AFTER INSERT ON products_sale
FOR EACH ROW
  begin
	DECLARE total_sale_value DECIMAL(65,30);
    DECLARE total_sale_out_value DECIMAL(65,30);
    DECLARE total_discount_value DECIMAL(65,30);
	
    SELECT SUM(ps.TotalPrice)
    INTO @total_sale_value 
    FROM teuscontroledb.products_sale ps
    INNER JOIN teuscontroledb.sales s
    ON ps.Id = s.Id
    WHERE ps.Id = NEW.Id;
    
    SELECT SUM(ps.TotalDiscount)
    INTO @total_discount_value 
    FROM teuscontroledb.products_sale ps
    INNER JOIN teuscontroledb.sales s
    ON ps.Id = s.Id
    WHERE ps.Id = NEW.Id;
    
    SELECT SUM(ps.TotalOutPrice)
    INTO @total_sale_out_value 
    FROM teuscontroledb.products_sale ps
    INNER JOIN teuscontroledb.sales s
    ON ps.Id = s.Id
    WHERE ps.Id = NEW.Id;
  
	UPDATE sales
    SET	TotalPrice = @total_sale_value,
		TotalOutPrice = @total_sale_out_value,
        TotalDiscount = @total_discount_value    
    WHERE Id = NEW.Id;
  end;
$$
DELIMITER ;


-- drop trigger calculate_total_sale_value_on_update;
DELIMITER $$
CREATE TRIGGER calculate_total_sale_value_on_update
AFTER UPDATE ON products_sale
FOR EACH ROW
  begin
	DECLARE total_sale_value DECIMAL(65,30);
    DECLARE total_sale_out_value DECIMAL(65,30);
    DECLARE total_discount_value DECIMAL(65,30);
	
    SELECT SUM(ps.TotalPrice)
    INTO @total_sale_value 
    FROM teuscontroledb.products_sale ps
    INNER JOIN teuscontroledb.sales s
    ON ps.Id = s.Id
    WHERE ps.Id = NEW.Id;
    
    SELECT SUM(ps.TotalDiscount)
    INTO @total_discount_value 
    FROM teuscontroledb.products_sale ps
    INNER JOIN teuscontroledb.sales s
    ON ps.Id = s.Id
    WHERE ps.Id = NEW.Id;
    
    SELECT SUM(ps.TotalOutPrice)
    INTO @total_sale_out_value 
    FROM teuscontroledb.products_sale ps
    INNER JOIN teuscontroledb.sales s
    ON ps.Id = s.Id
    WHERE ps.Id = NEW.Id;
  
	UPDATE sales
    SET	TotalPrice = @total_sale_value,
		TotalOutPrice = @total_sale_out_value,
        TotalDiscount = @total_discount_value    
    WHERE Id = NEW.Id;
  end;
$$
DELIMITER ;

SELECT * from users u;


INSERT INTO
	teuscontroledb.users
(Name,
	BirthDate,
	ProfileImage,
	ProfileType,
	Password,
	Email,
	Active,
	Deleted,
	CreatedDate,
	LastChange,
	CreatedBy)
VALUES('Usuario Administrador',
'2001-11-27 00:00:00',
'https://ciclovivo.com.br/wp-content/uploads/2018/10/iStock-536613027-696x464.jpg',
'Admin',
'AQAAAAEAACcQAAAAEBk6g+HOnrf1xQioBXKKBwj2MK8JtYAx3UUsWI31fz5dUQO2ZY/B8GTI90AhS0YXfw==',
'admin@admin.com',
1,
0,
'2022-10-04 22:52:24.064144',
null,
null);



