using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class TriggersProductsSaleAndProductsEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

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

            ");
            
            migrationBuilder.Sql(@"

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

            ");
            
            migrationBuilder.Sql(@"

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

            ");
            
            migrationBuilder.Sql(@"

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

            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger teuscontroledb.calculate_total_entry_value_on_insert;");
            migrationBuilder.Sql(@"drop trigger teuscontroledb.calculate_total_entry_value_on_update;");
            migrationBuilder.Sql(@"drop trigger teuscontroledb.calculate_total_sale_value_on_insert;");
            migrationBuilder.Sql(@"drop trigger teuscontroledb.calculate_total_sale_value_on_update;");
        }
    }
}
