USE [BDDicon]
GO
/****** Object:  StoredProcedure [dbo].[sp_Mam_TV002]    Script Date: 29/12/2019 21:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop procedure sp_Mam_TV002
ALTER PROCEDURE [dbo].[sp_Mam_TV002] (@tipo int,@vcnumi int=-1,@vcidcore int=-1,@vcsector int=-1,
@vcSecNumi int=-1,@vcnumivehic int=-1,@vcalm int =-1,
@vcfdoc date=null,@vcclie int=-1,
@vcfvcr date=null,@vctipo int=-1,@vcest int=-1,@vcobs nvarchar(50)='',@vcdesc decimal(18,2)=0,
@vctotal decimal(18,2)=0,@TV0021 TV0021Type Readonly,@vcuact nvarchar(10)='',
@numiVenta int=-1,@tipoC int=-1,@sucursalC int=-1,@ClienteCredito int=-1,@sucursal int=-1,@numerofactura int=-1,
@TV0022 TV0022Type Readonly,@TV0023 TV0022Type_Cabana Readonly,@vcfactura int=-1,@vcfactanul int=-1,@tipoFactura int=-1,
@fechaI  date=null,@fechaF  date=null,
@numiServ int=-1,@vcmoneda int=-1,@vcbanco int=-1,@modulo int=-1)

AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))
	DECLARE @newFecha date
	set @newFecha=GETDATE()

	IF @tipo=-1 --ELIMINAR REGISTRO
	BEGIN
		BEGIN TRY 
		update TV002 set vcest =-1 where vcnumi =@vcnumi 
			--DELETE from TV002 where vcnumi  =@vcnumi
			 
			 
			if  @vcsector = 2  --Administracion y socios
			begin
			  --  update dbdies.dbo.tcs015 set sfrec = 0, sfest = 0
				update dbdies.dbo.tcs015 set sfrec = 0, sfest = 1, sfsaldo = sfmonto
				where sfrec = (select fvanfac from tfv001 where fvanumi = @vcnumi)
				and sffdoc = @vcfdoc
				update dbdies.dbo.tcs014 set serec = 0, seest = 1, sesaldo = seimp1
				where serec = (select fvanfac from tfv001 where fvanumi = @vcnumi)
				and sefec = @vcfdoc
			end

			DELETE FROM TV0021 WHERE vdvc2numi  =@vcnumi 

			---------------SOLO EN CASO DE PRUEBAS ELIMINAR FACTURA 27/12/2017--------------
			DELETE FROM TFV001 where TFV001 .fvanumi =@vcnumi 
			delete from TFV0011 where TFV0011 .fvbnumi =@vcnumi 

			delete from TV0022 where TV0022.vetv2numi =@vcnumi 
			-------------------------------------------------
			select @vcnumi as newNumi  --Consultar que hace newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@vcuact)
		END CATCH
	END
		IF @tipo=-2 --ELIMINAR REGISTRO
	BEGIN
		BEGIN TRY 			 	 


			  --  update dbdies.dbo.tcs015 set sfrec = 0, sfest = 0
				update dbdies.dbo.tcs015 set sfrec = 0, sfest = 1, sfsaldo = sfmonto
				where sfrec = (select fvanfac from tfv001 where fvanumi = @vcnumi)
				and 2 = (select vcsector from tv002, tfv001 where tv002.vcnumi = tfv001.fvanumi2 and tfv001.fvanumi = @vcnumi) 
				and sffdoc = @vcfdoc

				update dbdies.dbo.tcs014 set serec = 0, seest = 1, sesaldo = seimp1
				where serec = (select fvanfac from tfv001 where fvanumi = @vcnumi)
				and 2 = (select vcsector from tv002, tfv001 where tv002.vcnumi = tfv001.fvanumi2 and tfv001.fvanumi = @vcnumi) 
				and sefec = @vcfdoc


		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@vcuact)
		END CATCH
	END
	IF @tipo=100 --NUEVO REGISTRO PARA DETALLE CABANA
	BEGIN
		BEGIN TRY 
			set @vcnumi=IIF((select COUNT(vcnumi) from TV002)=0,0,(select MAX(vcnumi) from TV002))+1

			--if (not Exists(select  a.*  from TV002 as a where a.vcsector =@vcsector and a.vcSecNumi  =@vcSecNumi and vcsector in (-10)))
			--begin

			INSERT INTO TV002 VALUES(@vcnumi ,@vcidcore ,@vcsector ,@vcSecNumi ,@vcnumivehic ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
			@vcest ,@vcobs ,@vcdesc ,@vctotal,@ClienteCredito,@vcfactura ,@vcfactanul,@vcmoneda,@vcbanco   )
			----INSERTO EL DETALLE

		    INSERT INTO TV0021 (vdvc2numi ,vdserv ,vdprod ,vdcmin ,vdpbas ,vdptot ,vdporc ,vddesc ,vdtotdesc ,vdobs ,vdpcos ,vdptot2 )
			SELECT @vcnumi,td.vdserv ,td.vdprod ,td.vdcmin ,td.vdpbas ,td.vdptot ,td.vdporc ,td.vddesc ,td.vdtotdesc ,
			td.vdobs ,td.vdpcos ,td.vdptot2  FROM @TV0021 AS td
			where td.estado =0

			insert into TV0022 (vetv2numi ,vesecnumi )
			select @vcnumi ,td.vesecnumi 
			from @TV0023 as td where td.estado =0

			select @vcnumi as newNumi

			--end
			--else
			--begin
			--	select -100  -----Venta insertada
			--end

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@vcuact)
		END CATCH
	END
	IF @tipo=1 --NUEVO REGISTRO PARA LOS DEMAS
	BEGIN
		BEGIN TRY 
			set @vcnumi=IIF((select COUNT(vcnumi) from TV002)=0,0,(select MAX(vcnumi) from TV002))+1
			if (not Exists(select  a.*  from TV002 as a where a.vcsector =@vcsector and a.vcSecNumi  =@vcSecNumi and vcsector in (3,4)))
			begin

			INSERT INTO TV002 VALUES(@vcnumi ,@vcidcore ,@vcsector ,@vcSecNumi ,@vcnumivehic ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
			@vcest ,@vcobs ,@vcdesc ,@vctotal,@ClienteCredito,@vcfactura  ,@vcfactanul ,@vcmoneda,@vcbanco )
			----INSERTO EL DETALLE

		    INSERT INTO TV0021 (vdvc2numi ,vdserv ,vdprod ,vdcmin ,vdpbas ,vdptot ,vdporc ,vddesc ,vdtotdesc ,vdobs ,vdpcos ,vdptot2 )
			SELECT @vcnumi,td.vdserv ,td.vdprod ,td.vdcmin ,td.vdpbas ,td.vdptot ,td.vdporc ,td.vddesc ,td.vdtotdesc ,
			td.vdobs ,td.vdpcos ,td.vdptot2  FROM @TV0021 AS td
			where td.estado =0

			insert into TV0022 (vetv2numi ,vesecnumi )
			select @vcnumi ,td.vesecnumi 
			from @TV0022 as td where td.estado =0

			select @vcnumi as newNumi
			end
			else
			begin
			select -100  -----Venta insertada

			end
			

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@vcuact)
		END CATCH
	END
	
	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRY 
				
			UPDATE TV002 SET vcsector =@vcsector ,vcSecNumi =@vcSecNumi,vcnumivehic =@vcnumivehic ,
			vcalm =@vcalm ,vcfdoc =@vcfdoc ,vcclie =@vcclie ,vcfvcr =@vcfvcr ,vctipo =@vctipo ,vcest =@vcest ,
			vcobs =@vcobs ,vcdesc =@vcdesc ,vctotal =@vctotal ,vcclietc9 =@ClienteCredito ,
			vcfactura =@vcfactura ,vcfactanul =@vcfactanul ,vcmoneda =@vcmoneda,vcbanco=@vcbanco 
			Where vcnumi = @vcnumi

			if(@vcfactura=0)
			begin
			update TFV001 set fvaest =@vcfactanul 
			where fvanumi =@vcnumi 
			end

		 ----------MODIFICO EL DETALLE DE EQUIPO------------
			--INSERTO LOS NUEVOS
		    INSERT INTO TV0021 (vdvc2numi ,vdserv ,vdprod ,vdcmin ,vdpbas ,vdptot ,vdporc ,vddesc ,vdtotdesc ,vdobs ,vdpcos ,vdptot2 )


			SELECT @vcnumi,td.vdserv ,td.vdprod ,td.vdcmin ,td.vdpbas ,td.vdptot ,td.vdporc ,td.vddesc ,td.vdtotdesc ,
			td.vdobs ,td.vdpcos ,td.vdptot2  FROM @TV0021 AS td
			where td.estado =0

					--MODIFICO LOS REGISTROS
			UPDATE TV0021
			SET vdprod =td.vdprod ,vdserv =td.vdserv ,vdcmin =td.vdcmin ,vdpbas=td.vdpbas ,vdptot =td.vdptot ,
			vdporc =td.vdporc ,vddesc =td.vddesc ,vdtotdesc =td.vdtotdesc ,vdobs =td.vdobs ,vdpcos =td.vdpcos ,
			vdptot2 =td.vdptot2 
			FROM TV0021  INNER JOIN @TV0021 AS td
			ON TV0021 .vdnumi     = td.vdnumi   and td.estado=2;

			--ELIMINO LOS REGISTROS
			DELETE FROM TV0021 WHERE vdnumi   in (SELECT td.vdnumi   FROM @TV0021 AS td WHERE td.estado=-1)


			select @vcnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),2,@newFecha,@newHora,@vcuact)
		END CATCH
	END

	IF @tipo=3 --MOSTRaR TODOS
	BEGIN
		BEGIN TRY
	



	
		select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal
		,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector
,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull(cliente .lanom,(select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ))  as lanom
		,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,vehiculo .lbplac ,a.vcalm ,
		a.vcclie ,a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal,isnull((select isnull(credito.cjnumi,0)
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito,
		a.vcfactura
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a inner join DBDies .dbo.TCL001 as cliente on cliente .lanumi =a.vcclie 
		inner join DBDies .dbo.TCL0011 as vehiculo on vehiculo.lblin =a.vcnumivehic and a.vcsector =3
		
		where a.vcest >=0
		union--VENTAS DE REMOLQUE,CRUZANDO CON LA TABLA DE CLIENTES DE REMOLQUES
			select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal 
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector ,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull(cliente .ranom,(select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ))  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,vehiculo .rbplac  as lbplac ,a.vcalm ,
		a.vcclie  ,a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a inner join DBDies .dbo.TCR001  as cliente on cliente .ranumi  =a.vcclie 
		inner join DBDies .dbo.TCR0011  as vehiculo on vehiculo.rblin  =a.vcnumivehic and a.vcsector =4
		
		where a.vcest >=0

		union--VENTAS DE REMOLQUE,SIN CRUZAR CON LA TABLA DE CLIENTES DE REMOLQUE
			select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal 
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector ,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull((select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ),'')  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,''  as lbplac ,a.vcalm ,
		a.vcclie  ,a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco
		from TV002 as a 
		where a.vcest >=0 AND a.vcsector =4 AND vcnumi not in(select x.vcnumi from TV002 as x inner join DBDies .dbo.TCR001  as cliente1 on cliente1 .ranumi  =x.vcclie 
																			  inner join DBDies .dbo.TCR0011  as vehiculo1 on vehiculo1.rblin  =x.vcnumivehic and x.vcsector =4
																			  where x.vcest >=0)
		
		union--del sector de socios,pero solo trae los que cuadran con la tabla de socios
			select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal 
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull(concat(cliente .cfapat,' ' ,cliente .cfamat ,' ',cliente .cfnom),(select fact.fvadescli1 from TFV001 as fact where fact.fvanumi =a.vcnumi ))  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,''  as lbplac ,a.vcalm  ,
		a.vcclie ,a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a inner join DBDies .dbo.TCS01   as cliente on cliente .cfnumi   =a.vcclie 
		 and a.vcsector =2
		where a.vcest >=0
		
		UNION--del sector de socios,pero treyendo a los que no cuadran con la tabla de socios
		select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal 
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull((select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ),'')  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,''  as lbplac ,a.vcalm  ,
		a.vcclie ,a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a 
		where a.vcest >=0 and a.vcsector =2 and 
			  a.vcnumi not in(select vcnumi from TV002 as x inner join DBDies .dbo.TCS01   as cliente on cliente .cfnumi   =x.vcclie 
					 and x.vcsector =2
					 where x.vcest >=0)

		union--SERVICIO DE CABAÑAS HACIENDO CRUCE CON CLIENTES DE CABAÑAS
			select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector ,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
			,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull(concat(cliente .haapat ,'' ,cliente .haamat  ,'',cliente .hanom ),(select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ))  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,''  as lbplac ,a.vcalm  ,
		a.vcclie  ,
		a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a inner join DBDies .dbo.TCH001    as cliente on cliente .hanumi    =a.vcclie 
		 and a.vcsector =-10
		where a.vcest >=0

		union--SERVICIO DE CABAÑAS SIN HACER CRUCE CON CLIENTES DE CABAÑAS
			select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector ,a.vcfdoc,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull((select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ),'')  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,''  as lbplac ,a.vcalm  ,
		a.vcclie  ,
		a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a  
		where a.vcest >=0 and a.vcsector =-10 and a.vcnumi not in (select x.vcnumi from TV002 as x inner join DBDies .dbo.TCH001    as cliente1 on cliente1 .hanumi    =x.vcclie 
																							 and x.vcsector =-10
																							where x.vcest >=0)

		union
			select a.vcnumi ,(select suc.cadesc  from DBDies .dbo.TC001 as suc where suc.canumi =a.vcalm ) as sucursal
			,IIF(a.vcsector =-10,'CABAÑAS',(select  aa.cedesc1 from DBDies .dbo.TC0051 as aa where aa.cecod1 =6
and aa.cecod2=1 and aa.cenum=a.vcsector))as sector
			,a.vcfdoc ,
		isnull((select fact.fvanfac  from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as factura
		,isnull((select fact.fvanitcli   from TFV001 as fact where fact.fvanumi =a.vcnumi ),0) as nit
		,isnull((select fact.fvadescli1    from TFV001 as fact where fact.fvanumi =a.vcnumi ),'')  as lanom
			,a.vcidcore ,a.vcsector ,a.vcSecNumi ,a.vcnumivehic ,''  as lbplac ,a.vcalm ,
		a.vcclie ,a.vcfvcr ,a.vctipo ,(select cedesc1   from DBDies .dbo.TC0051  as ba where ba.cecod1 =14 and ba.cecod2 =3 and ba.cenum  =a.vctipo ) as tipo,a.vcest ,a.vcobs ,a.vcdesc ,a.vctotal 
,isnull((select isnull(credito.cjnumi,0) 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),0)
		 as numicredito,isnull((select isnull(credito.cjnombre ,'') 
		from  TC009 as credito where  credito .cjnumi =a.vcclietc9 ),'')as clientecredito
		,a.vcfactura 
		,isnull(vcfactanul,1) as anulada,isnull(a.vcmoneda,1)as  vcmoneda ,isnull(a.vcbanco,0) as vcbanco,
		isnull((select concat (banc.canombre,' ',banc.cacuenta  )  from DBDies .dbo.BA001 as banc where banc.canumi =a.vcbanco),'') as banco

		from TV002 as a where a.vcsector not in(3,4,2,-10) and 
		 a.vcest >=0 
		order by vcnumi asc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=4 --MOSTRaR DETALLE de la venta de servicios con personal y sin personal
	BEGIN
		BEGIN TRY
		
			SELECT vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,b.eddesc as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
			,vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado
			from TV0021,DBDies .dbo.TCE004 as b,TV002 as a where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi  and a.vcnumi =@vcnumi and a.vcsector not in (2,-10)  ----2=Socios   10=Cabañas

			union

			select vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,c.ldcdprod1 as descripcion  ,vdcmin ,vdpbas,vdptot ,vdporc  ,vddesc  ,vdtotdesc  
			,vdobs  ,vdpcos  ,vdptot2  ,1 as estado
             from TV0021 as a 
             inner join DBDies.dbo.TCL003 as c on vdprod   =c.ldnumi   and vdvc2numi  =@vcnumi 
			 inner join TV002 as b on b.vcnumi =@vcnumi and b.vcsector not in (2,-10)----2=Socios   10=Cabañas
		union
			--detalle por mes del pago de socios
			SELECT vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,concat(b.eddesc,' MES ',UPPER(DateName( month , DateAdd( month , cuota .semes  , -1 ) ))
		,' - ',cuota.seano) as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
			,vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado
			from TV0021,DBDies .dbo.TCE004 as b,TV002 as a,DBDies .dbo.TCS014 as cuota where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi  and a.vcnumi =@vcnumi and a.vcsector =2
			 and vdserv =1 and cuota.selin =vdprod 
			union
			--detalle por gestion del pago de socios
			SELECT vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,concat(b.eddesc,' GESTION ',cuota.sfgestion ) as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
			,vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado
			from TV0021,DBDies .dbo.TCE004 as b,TV002 as a,DBDies .dbo.TCS015 as cuota where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi  and a.vcnumi =@vcnumi and a.vcsector =2
			 and vdserv =2 and cuota.sflin =vdprod  
			
			UNION
			--detalle de socios de los servicios >2 
			SELECT vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,b.eddesc as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
			,vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado
			from TV0021,DBDies .dbo.TCE004 as b,TV002 as a where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi  and a.vcnumi =@vcnumi and a.vcsector =2 and vdserv>2

				union--cabañas,los que hace cuadre con clientes de hotel

			select vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,Concat('ALQUILER DE LA ',cabana.hbnom ,' DEL ',
c.hdfcin ,' AL ',c.hdfcou ) as descripcion  ,vdcmin ,vdpbas,vdptot ,vdporc  ,vddesc  ,vdtotdesc  
			,vdobs  ,vdpcos  ,vdptot2  ,1 as estado
             from TV0021 as a 
             inner join DBDies.dbo.TCH003  as c on  vdvc2numi  =@vcnumi 
			 inner join TV002 as b on b.vcnumi =@vcnumi and b.vcsector =-10----2=Socios   10=Cabañas
			 inner Join DBDies .dbo.TCH002 as cabana on cabana .hbnumi =c.hdtc2cab 
			 and c.hdnumi =a.vdprod --b.vcSecNumi 

			 union--cabañas,pero sin hacer cuadre con clientes de hotel

			SELECT vdnumi  ,vdvc2numi  ,vdserv ,vdprod   ,b.eddesc as descripcion,vdcmin ,vdpbas ,vdptot  ,vdporc  ,vddesc  
			,vdtotdesc  ,vdobs  ,vdpcos  ,vdptot2 ,1 as estado
			from TV002 ,TV0021,DBDies .dbo.TCE004 as b where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi and vcnumi=vdvc2numi and vcsector=-10 and 
			 vdnumi not in (select a1.vdnumi  from TV0021 as a1
					 inner join DBDies.dbo.TCH003  as c1 on  a1.vdvc2numi  =@vcnumi 
					 inner join TV002 as b1 on b1.vcnumi =@vcnumi and b1.vcsector =-10----2=Socios   10=Cabañas
					 inner Join DBDies .dbo.TCH002 as cabana1 on cabana1 .hbnumi =c1.hdtc2cab 
					 and c1.hdnumi =b1.vcSecNumi ) 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

	IF @tipo=5 --LISTAR VENTAS LAVADERO 
	BEGIN
		BEGIN TRY
		
	select a.ldnumi ,a.ldnord ,a.ldtcl11veh ,vehiculo .lbplac as placas,a.ldsuc ,a.ldfdoc ,a.ldtcl1cli ,cliente .lanom as nombre,
	a.ldfvcr ,a.ldtven  ,a.ldtmon ,a.ldest ,Sum(detalle .lcptot)as total ,isnull (a.ldbanco,0) as ldbanco
	,isnull((select concat(aa.canombre ,' ',aa.cacuenta) from dbdies.dbo.BA001 as aa where aa.canumi =a.ldbanco),' ') as banco
	from DBDies .dbo.TCL002 as a ,DBDies .dbo.TCL0021 as detalle,DBDies .dbo.TCL001 as cliente,
	DBDies .dbo.TCL0011 as vehiculo  where  -----vcsector1=Lavadero
	 detalle .lcnumi =a.ldnumi and cliente.lanumi =a.ldtcl1cli
	and vehiculo .lblin =a.ldtcl11veh
		--and Year(a.ldfdoc)=Year(GetDate()) and MONTH (a.ldfdoc )>=  Month(DATEADD(Month,-1,GetDate()))
	and a.ldfdoc > '01/11/2018' -- Verificar posteriormente
	and a.ldnumi not in (  select detalle .vesecnumi    
	from TV002 as venta inner join TV0022 as detalle on detalle .vetv2numi =venta.vcnumi and vcsector =3 and vcest >=0
	inner join TFV001 as fact on fact.fvanumi =venta.vcnumi and fact.fvaest =1
	)
	and a.ldtipo=1	
	group by a.ldnumi ,a.ldnord ,a.ldtcl11veh ,a.ldsuc ,a.ldfdoc ,a.ldbanco,a.ldtcl1cli ,a.ldtmon ,a.ldfvcr ,a.ldtven ,a.ldest,vehiculo .lbplac ,cliente .lanom 
	order by a.ldnumi desc 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=6 --MOSTRaR DETALLE de la venta de servicios con personal y sin personal
	BEGIN
		BEGIN TRY
		
			SELECT lclin ,lcnumi ,lctce4pro,lctcl3pro  ,b.eddesc  ,lctp1emp,lctce42pro,'' as nombre 
			,lcpuni ,isnull(lccant,1) as lccant,isnull(lcpdes,0) as lcpdes,isnull(lcmdes,0) as lcmdes,lcptot 
			,lcfpag ,lcppagper ,lcmpagper ,lcest
			from DBDies .dbo. TCL0021,DBDies .dbo.  TCE004 as b where lcnumi =@numiVenta  
			 and lctce4pro =b.ednumi 

			union

			select lclin ,lcnumi ,lctce4pro,lctcl3pro  ,c.ldcdprod1   ,lctp1emp,lctce42pro,'' as nombre 
			,lcpuni ,isnull(lccant,1) as lccant,isnull(lcpdes,0) as lcpdes,isnull(lcmdes,0) as lcmdes,lcptot 
			,lcfpag ,lcppagper ,lcmpagper ,lcest
             from DBDies .dbo. TCL0021 as a 
             inner join DBDies .dbo. TCL003 as c on lctcl3pro =c.ldnumi  and lcnumi =@numiVenta  
			
			
			order by lclin 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=7 
	BEGIN
		BEGIN TRY
		select a.cnnum,a.cndesc1 
		from TC0051 as a where a.cncod1 =8 and a.cncod2 =1
		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=8 --REPORTE DE FACTURA 
	BEGIN
		BEGIN TRY
	SELECT vdcmin AS Cantidad, b.eddesc AS detalle, vdpbas AS PrecioUnitario, vdtotdesc AS Total, vdnumi, Cast('' AS image) AS img
FROM     TV0021, DBDies.dbo.TCE004 AS b,TV002 as a
WHERE  vdvc2numi = @vcnumi  AND vdserv = b.ednumi
  and  a.vcnumi =vdvc2numi 
				  and a.vcsector in(3,4,8)
UNION ALL
SELECT vdcmin AS Cantidad, c.ldcdprod1 AS detalle, vdpbas AS PrecioUnitario, vdtotdesc AS Total, vdnumi, Cast('' AS image) AS img
FROM     TV0021 AS a INNER JOIN
                  DBDies.dbo.TCL003 AS c ON vdprod = c.ldnumi AND vdvc2numi = @vcnumi
				  inner join TV002 as b on b.vcnumi =a.vdvc2numi 
				  and b.vcsector in(3,4,8)
UNION ALL
SELECT detalle .vdcmin as Cantidad,b.eddesc  as detalle, detalle.vdpbas as PrecioUnitario,detalle.vdtotdesc as Total,detalle.vdnumi , Cast('' AS image) AS img
			from TV0021 as detalle,DBDies .dbo.TCE004  as b,TV002 as a where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi    and a.vcnumi =@vcnumi  and a.vcsector not in(3,4,8) and a.vcsector >0

--para el sector de cabañas sin cruzar con cabañas
UNION ALL
SELECT detalle .vdcmin as Cantidad,b.eddesc  as detalle, detalle.vdpbas as PrecioUnitario,detalle.vdtotdesc as Total,detalle.vdnumi , Cast('' AS image) AS img
			from TV0021 as detalle,DBDies .dbo.TCE004  as b,TV002 as a where vdvc2numi  =@vcnumi 
			 and vdserv  =b.ednumi    and a.vcnumi =@vcnumi  and a.vcsector =-10 and a.vcclie =0
			
UNION ALL
				  SELECT  detalle .vdcmin as Cantidad,Concat('ALQUILER DE LA ',cabana.hbnom ,' DEL ',
b.hdfcin ,' AL ',b.hdfcou )  as detalle, detalle.vdpbas as PrecioUnitario,detalle.vdtotdesc as Total,detalle.vdnumi , Cast('' AS image) AS img
			from TV0021 as detalle,DBDies .dbo.TCH003  as b,TV002 as a,DBDies .dbo.TCH002 as cabana where 
			     a.vcsector <0 and detalle .vdvc2numi =a.vcnumi
				 and detalle.vdprod =b.hdnumi 
				 and a.vcnumi =@vcnumi
				and cabana .hbnumi = b.hdtc2cab --detalle .vdprod 
				order by vdnumi 
		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

	IF @tipo=9 --LISTAR VENTAS REMOQLUE 
	BEGIN
		BEGIN TRY
		
	select a.renumi  ,a.rencont ,a.retcr11vehcli  ,vehiculo .rbplac  as placas,1 as sucursal ,a.refdoc  ,a.retcr1cli  ,cliente .ranom  as nombre,
	a.refvcr  ,a.retpago  ,a.reest  ,Sum(detalle .rfprec -detalle .rfmdesc )as total ,cliente.ranit ,cliente.rafacnom 
	from DBDies .dbo.TCR003  as a ,DBDies .dbo.TCR0031  as detalle,DBDies .dbo.TCR001 as cliente,
	DBDies .dbo.TCR0011  as vehiculo  where   -----vcsector1=Lavadero
	 detalle .rfnumi =a.renumi  and cliente.ranumi  =a.retcr1cli 
	and vehiculo .rblin  =a.retcr11vehcli  
	--and Year(a.refdoc )=Year(GetDate()) and MONTH (a.refdoc )>=  Month(DATEADD(Month,-4,GetDate()))
	and a.refdoc > '01/07/2018'  --Revisar
	and a.renumi  not in (  select detalle .vesecnumi    
	from TV002 as venta inner join TV0022 as detalle on detalle .vetv2numi =venta.vcnumi and vcsector =4 and vcest >=0
	inner join TFV001 as fact on fact.fvanumi =venta.vcnumi and fact.fvaest =1
	) 
	group by a.renumi ,a.rencont  ,a.retcr11vehcli  ,vehiculo.rbplac ,a.refdoc ,a.retcr1cli ,cliente.ranom ,a.refvcr ,a.retpago ,a.reest,cliente.ranit ,cliente.rafacnom   
	order by a.renumi  desc 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END


IF @tipo=10 --MOSTRaR DETALLE de la venta de servicios Remolque
	BEGIN
		BEGIN TRY
		
			SELECT a.rflin  ,a.rfnumi  ,a.rfTCE04Serv ,b.eddesc,a.rfprec  ,1 as cantidad,a.rfpdesc  ,a.rfmdesc  ,(a.rfprec -a.rfmdesc )as subtotal 
			from DBDies .dbo. TCR0031 as a,DBDies .dbo.  TCE004 as b where a.rfnumi  =@numiVenta  
			 and a.rfTCE04Serv  =b.ednumi 
			
			order by a.rflin asc 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=11 --LISTAR SERVICIOS DEL DICONTA
	BEGIN
		BEGIN TRY
	select servicio .ednumi as sdnumi,servicio .eddesc as sddesc,servicio .edprec as sdprec
	,isnull((select isnull((cuenta.senumi),0)   from TS006 as cuenta where cuenta.seest =1 and cuenta .senumiserv =servicio .ednumi and
	cuenta.senrocuenta>0),0)
	as estado
	from DBDies .dbo.TCE004 as servicio 
	where servicio .edtipo = @tipoC 
		--and servicio .ednumi  not in (select td.vdserv  from @TV0021 as td)'comentado para que no oculte los servicios q ya elijieron al momento de hacer la venta
		and servicio.edsuc =@sucursalC 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
IF @tipo=12 --LISTAR VENTAS CABANAS 
	BEGIN
		BEGIN TRY
			select 
				a.hdnumi, a.hdfing, cliente.hanumi, cliente.hanom as cliente, a.hdfcin as fechai,a.hdfcou as fechaf,
				a.hdtc2cab as numiCabana, cabana.hbnom, isnull(a.hdtotal, 0) as precio, 1 as cantidadDias, isnull(a.hdtotal, 0) as total
				/*isnull(a.hdprecio, 0) as precio, DATEDIFF(day, a.hdfcin, a.hdfcou) + 1 as cantidadDias, 
				(isnull(a.hdprecio, 0) * (DATEDIFF(day, a.hdfcin, a.hdfcou) + 1)) as total*/
			from 
				DBDies.dbo.TCH003 as a inner join DBDies.dbo.TCH001 as cliente on cliente.hanumi=a.hdtc1cli
				inner join DBDies.dbo.TCH002 as cabana on cabana.hbnumi=a.hdtc2cab and a.hdprecio>0 
				--	and Year(a.hdfcin)>=Year(GetDate()) --and MONTH (a.hdfcin )>=  Month(DATEADD(Month,-1,GetDate()))
				and a.hdfcin > '01/09/2018'  --Revisar
				and 
				a.hdnumi not in (select 
									aa.vesecnumi 
								 from 
									TV002 as b, TV0022 as aa,TFV001 as fact where aa.vetv2numi=b.vcnumi and b.vcsector=-10 and b.vcest>=0 and 
									 fact.fvanumi =b.vcnumi and fact.fvaest =1)
				
			order by a.hdnumi desc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH
	END

IF @tipo=13 --Verificar Si es Factura Manual
	BEGIN
		BEGIN TRY
		
select dosificacion .sbnumi ,dosificacion .sbcia ,dosificacion .sbalm ,dosificacion .sbautoriz ,dosificacion .sbinicio ,dosificacion .sbfinal ,
dosificacion .sbfal ,
dosificacion .sbfdel
from TS002  as dosificacion where dosificacion.sbtipo =0  and dosificacion .sbalm =@sucursal
and @vcfdoc>= dosificacion.sbfdel and @vcfdoc <= dosificacion.sbfal
and dosificacion.sbnumi in 
(select sbnumi from TS002 as x where x.sbmodulo in (
select m.id  from Modulos as m inner join DetalleModulo as detalle on detalle.modulosid =m.id and detalle .modulo =@modulo))
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=14 --Verificar Si es Factura Manual
	BEGIN
		BEGIN TRY
		
select factura.*
from TFV001 as factura inner join TV002 as venta
on venta.vcnumi =factura .fvanumi 
and venta.vcfactura =0 and  factura .fvaalm =@sucursal and factura .fvanfac =@numerofactura
and venta.vcfdoc >=@fechaI and venta .vcfdoc <=@fechaF 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=15 --MOSTRAR LAS VENTAS PARA SELECCIONAR MAS DE UNA EN EL FACTURADOR LAVADERO
	BEGIN
		BEGIN TRY
		select  a.venumi ,a.vetv2numi ,a.vesecnumi,venta .ldnord as orden,
		vehiculo.lbplac as placa,cliente.lanom as cliente ,1 as estado
		from TV0022 as a 
		inner join DBDies .dbo.TCL002 as venta on venta.ldnumi =a.vesecnumi 
		inner join DBDies .dbo.TCL0011 as vehiculo on vehiculo .lblin =venta.ldtcl11veh 
		inner join DBDies .dbo.TCL001 as cliente on cliente.lanumi =venta.ldtcl1cli 
		where a.vetv2numi =@vcnumi 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=16 --MOSTRAR LAS VENTAS PARA SELECCIONAR MAS DE UNA EN EL FACTURADOR LAVADERO
	BEGIN
		BEGIN TRY
		select  a.venumi ,a.vetv2numi ,a.vesecnumi,venta .rencont  as orden,
		vehiculo.rbplac  as placa,cliente.ranom  as cliente ,1 as estado
		from TV0022 as a 
		inner join DBDies .dbo.TCR003  as venta on venta.renumi  =a.vesecnumi 
		inner join DBDies .dbo.TCR0011  as vehiculo on vehiculo .rblin  =venta.retcr11vehcli  
		inner join DBDies .dbo.TCR001  as cliente on cliente.ranumi  =venta.retcr1cli  
		where a.vetv2numi =@vcnumi 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END



IF @tipo=17 --MOSTRAR LAS VENTAS PARA SELECCIONAR MAS DE UNA EN EL FACTURADOR LAVADERO
	BEGIN
		BEGIN TRY
		select  a.venumi ,a.vetv2numi ,a.vesecnumi,cliente.hanom   as cliente,venta.hdfcin as fechai,
		venta.hdfcou as fechaf ,cabana.hbnom as cabana,1 as estado
		from TV0022 as a 
		inner join DBDies .dbo.TCH003  as venta on venta.hdnumi   =a.vesecnumi 
		
		inner join DBDies .dbo.TCH001   as cliente on cliente.hanumi   =venta.hdtc1cli 
		inner join DBDies .dbo. TCH002 as cabana on cabana .hbnumi =venta.hdtc2cab  
		where a.vetv2numi =@vcnumi 



		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
IF @tipo=18 
	BEGIN
		BEGIN TRY
		
select isnull(Max(factura.fvanfac)+1,(select top 1 dosi.sbinicio from TS002 as dosi where dosi.sbalm=@sucursal and dosi.sbfdel=@fechaI and dosi.sbfal=@fechaF)) as nro
from TFV001 as factura inner join TV002 as venta
on venta.vcnumi =factura .fvanumi 
and venta.vcfactura =0 and  factura .fvaalm =@sucursal 
and venta.vcfdoc >=@fechaI and venta .vcfdoc <=@fechaF 
 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
IF @tipo=19 
	BEGIN
		BEGIN TRY
		
select a.ServFactAnulada as ServicioAnulado,
servicio.eddesc as servicio
from SY000 as a
inner join DBDies .dbo.TCE004 as servicio on servicio.ednumi=a.ServFactAnulada 
 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=19 
	BEGIN
		BEGIN TRY
		
select a.ServFactAnulada as ServicioAnulado,
servicio.eddesc as servicio
from SY000 as a
inner join DBDies .dbo.TCE004 as servicio on servicio.ednumi=a.ServFactAnulada 
 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
IF @tipo=20
	BEGIN
		BEGIN TRY
select a.*
from TS002 as a where a.sbalm=@sucursal and a.sbtipo=@tipoFactura

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

	--codigo danny
	IF @tipo=21
	BEGIN
		BEGIN TRY
			select senumi,senumiserv,senrocuenta,seest,seref,sefactu
			from TS006
			where senumiserv=@numiServ

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
					VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH
	END

	IF @tipo=22 --Verificar Si es Factura Automatica
	BEGIN
		BEGIN TRY
		
SELECT top 1 * FROM TS002 WHERE sbcia = 1 AND sbalm = @sucursal  AND sbfdel <= @vcfdoc AND sbfal >= @vcfdoc AND sbest = 1 and sbtipo=1
and sbnumi in 
(select sbnumi from TS002 as x where x.sbmodulo in (
select m.id  from Modulos as m inner join DetalleModulo as detalle on detalle.modulosid =m.id and detalle .modulo =@modulo))
order by sbnumi desc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END


	IF @tipo=23 --LISTAR VENTAS LAVADERO 
	BEGIN
		BEGIN TRY
		
	select a.ldnumi ,a.ldnord ,a.ldtcl11veh ,vehiculo .lbplac as placas,a.ldsuc ,a.ldfdoc ,a.ldtcl1cli ,cliente .lanom as nombre,
	a.ldfvcr ,a.ldtven  ,a.ldtmon ,a.ldest ,Sum(detalle .lcptot)as total ,isnull (a.ldbanco,0) as ldbanco
	,isnull((select concat(aa.canombre ,' ',aa.cacuenta) from dbdies.dbo.BA001 as aa where aa.canumi =a.ldbanco),' ') as banco
	from DBDies .dbo.TCL002 as a ,DBDies .dbo.TCL0021 as detalle,DBDies .dbo.TCL001 as cliente,
	DBDies .dbo.TCL0011 as vehiculo  where  -----vcsector1=Lavadero
	 detalle .lcnumi =a.ldnumi and cliente.lanumi =a.ldtcl1cli
	and vehiculo .lblin =a.ldtcl11veh
		--and Year(a.ldfdoc)=Year(GetDate()) and MONTH (a.ldfdoc )>=  Month(DATEADD(Month,-1,GetDate()))
	and a.ldfdoc > '01/11/2018' -- Verificar posteriormente
	and a.ldnumi not in (  select detalle .vesecnumi    
	from TV002 as venta inner join TV0022 as detalle on detalle .vetv2numi =venta.vcnumi and vcsector =8 and vcest >=0
	inner join TFV001 as fact on fact.fvanumi =venta.vcnumi and fact.fvaest =1
	)
	and a.ldtipo=0

	
	group by a.ldnumi ,a.ldnord ,a.ldtcl11veh ,a.ldsuc ,a.ldfdoc ,a.ldbanco,a.ldtcl1cli ,a.ldtmon ,a.ldfvcr ,a.ldtven ,a.ldest,vehiculo .lbplac ,cliente .lanom 
	order by a.ldnumi desc 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
End

