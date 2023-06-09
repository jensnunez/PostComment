USE [Gk_Geek2PDSA]
GO
/****** Object:  Table [dbo].[auditoria]    Script Date: 31/03/2023 4:12:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[auditoria](
	[usuario] [varchar](20) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[descripcion] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comments]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[postId] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[body] [varchar](max) NOT NULL,
 CONSTRAINT [PK_comments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[grupo]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grupo](
	[Usuario] [varchar](20) NULL,
	[Cedula] [varchar](15) NULL,
	[Nombres] [varchar](100) NULL,
	[Apellidos] [varchar](100) NULL,
	[Genero] [varchar](1) NULL,
	[Parentesco] [varchar](20) NULL,
	[Edad] [int] NULL,
	[MenorEdad] [bit] NULL,
	[FechaNacimiento] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[posts]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[title] [varchar](100) NOT NULL,
	[body] [varchar](max) NOT NULL,
 CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](20) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[email] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[username] [varchar](20) NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[usuario] [varchar](20) NOT NULL,
	[clave] [varchar](20) NOT NULL,
	[correo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_comment]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create  proc [dbo].[sp_delete_comment] (@idcomments int) as

If  Exists (Select * from comments (nolock) where id = @idcomments )  begin
		delete from  comments    where id = @idcomments
		
end


GO
/****** Object:  StoredProcedure [dbo].[sp_delete_post]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create  proc [dbo].[sp_delete_post] (@postid int) as

If  Exists (Select * from posts (nolock) where id = @postid )  begin
		delete from  posts    where id = @postid
		
end


GO
/****** Object:  StoredProcedure [dbo].[sp_edit_comment]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_edit_comment] (@idcomments int, @postid int, @name varchar(100), @body varchar(max), @email varchar(100)) as

If  Exists (Select * from comments (nolock) where id = @idcomments )  begin
		update comments set postId=@postid,name=@name,body=@body, email= @email   where id = @postid
		
end


GO
/****** Object:  StoredProcedure [dbo].[sp_edit_post]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_edit_post] (@postid int, @userid int, @title varchar(100), @body varchar(max)) as

If  Exists (Select * from posts (nolock) where id = @postid )  begin
		update posts set userid=@userid,title=@title,body=@body   where id = @postid
		
end


GO
/****** Object:  StoredProcedure [dbo].[sp_editar_grupos]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE  proc [dbo].[sp_editar_grupos] (@usuario varchar(20), @cedula varchar(20),@nombres varchar(100),@apellidos varchar(100),
@genero varchar(1), @parentesco varchar(20),@fecha date, @edad int
) as

If  Exists (Select * from grupo (nolock) where usuario=@usuario )  begin
		update grupo 
		set Cedula= @cedula, nombres=@nombres, Apellidos=@apellidos, Genero=@genero, Parentesco=@parentesco, FechaNacimiento=@fecha, Edad=@edad 
		where  usuario=@usuario 
		insert into auditoria
		values(@usuario,getdate(), 'Grupo actualizado')

end else begin
		insert into auditoria
		values(@usuario,getdate(), 'Este Grupo no se puede actualizar porque no existe')
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_editar_usuario]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_editar_usuario] (@correo varchar(20), @clave varchar(20), @usuario varchar(20)) as

If  Exists (Select * from usuario (nolock) where username=@usuario )  begin
		update usuario set password=@clave, email=@correo where  username=@usuario 
end


GO
/****** Object:  StoredProcedure [dbo].[sp_editar_usuarios]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_editar_usuarios] (@correo varchar(20), @clave varchar(20), @usuario varchar(20)) as

If  Exists (Select * from usuarios (nolock) where usuario=@usuario )  begin
		update usuarios set clave=@clave, correo=@correo where  usuario=@usuario 
		insert into auditoria
		values(@usuario,getdate(), 'Usuario actualizado')

end else begin
		insert into auditoria
		values(@usuario,getdate(), 'Este Usuario no se puede actualizar porque no existe')
	
end



GO
/****** Object:  StoredProcedure [dbo].[sp_eliminar_grupos]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create  proc [dbo].[sp_eliminar_grupos] ( @usuario varchar(20)) as

If  Exists (Select * from grupo (nolock) where usuario=@usuario )  begin
		delete from  grupo  where  usuario=@usuario 
		insert into auditoria
		values(@usuario,getdate(), 'Grupo eliminado')

end else begin
		insert into auditoria
		values(@usuario,getdate(), 'Este Grupo no se puede eliminar porque no existe')
	
end


GO
/****** Object:  StoredProcedure [dbo].[sp_eliminar_usuario]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_eliminar_usuario] ( @usuario varchar(20)) as

If  Exists (Select * from usuario (nolock) where username=@usuario )  begin
		delete from  usuario  where  username=@usuario 
end


GO
/****** Object:  StoredProcedure [dbo].[sp_eliminar_usuarios]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create  proc [dbo].[sp_eliminar_usuarios] ( @usuario varchar(20)) as

If  Exists (Select * from usuarios (nolock) where usuario=@usuario )  begin
		delete from  usuarios  where  usuario=@usuario 
		insert into auditoria
		values(@usuario,getdate(), 'Usuario eliminado')

end else begin
		insert into auditoria
		values(@usuario,getdate(), 'Este Usuario no se puede eliminar porque no existe')
	
end


GO
/****** Object:  StoredProcedure [dbo].[sp_guardar_grupos]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE  proc [dbo].[sp_guardar_grupos] (@usuario varchar(20), @cedula varchar(20),@nombres varchar(100),@apellidos varchar(100),
@genero varchar(1), @parentesco varchar(20),@fecha date='9999-01-01', @edad int
) as

If not Exists (Select * from grupo (nolock) where usuario = @usuario and Parentesco=@parentesco )  begin
		
		declare @menoredad bit =1
		if (@edad < 18) begin
		  select @menoredad=0
		end
		INSERT INTO [dbo].[grupo]
           ([Usuario],[Cedula],[Nombres],[Apellidos],[Genero],[Parentesco],[Edad],[MenorEdad],[FechaNacimiento])
		values (@usuario,@cedula,@nombres,@apellidos,@genero,@parentesco,@edad,@menoredad,@fecha)
		insert into auditoria
		values(@usuario,getdate(), 'Registro grupo creado')
end
else begin 
		insert into auditoria
		values(@usuario,getdate(), 'Este Grupo ya ha sido creado anteriormente')
end



GO
/****** Object:  StoredProcedure [dbo].[sp_guardar_usuario]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_guardar_usuario] (@correo varchar(20), @clave varchar(20),@usuario varchar(20)) as

If not Exists (Select * from usuario (nolock) where username = @usuario )  begin
		Insert into usuario(email,password, username)
		values(@correo,@clave,@usuario)
		insert into auditoria
		values(@usuario,getdate(), 'Registro creado')
end


GO
/****** Object:  StoredProcedure [dbo].[sp_guardar_usuarios]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_guardar_usuarios] (@correo varchar(20), @clave varchar(20),@usuario varchar(20)) as

If not Exists (Select * from usuarios (nolock) where usuario = @usuario )  begin
		Insert into usuarios(correo,clave, usuario)
		values(@correo,@clave,@usuario)
		insert into auditoria
		values(@usuario,getdate(), 'Registro creado')
end
else begin 
		insert into auditoria
		values(@usuario,getdate(), 'Este Usuario ya ha sido creado anteriormente')
end



GO
/****** Object:  StoredProcedure [dbo].[sp_list_comments]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create  proc [dbo].[sp_list_comments]  as

select * from comments


GO
/****** Object:  StoredProcedure [dbo].[sp_list_commentsPost]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE  proc [dbo].[sp_list_commentsPost](@postId int)  as

select * from comments where postId=@postId


GO
/****** Object:  StoredProcedure [dbo].[sp_list_post]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create  proc [dbo].[sp_list_post]  as

select * from posts


GO
/****** Object:  StoredProcedure [dbo].[sp_lista_auditoria]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_lista_auditoria]  as

	select  usuario, fecha, descripcion from auditoria
	




GO
/****** Object:  StoredProcedure [dbo].[sp_lista_usuarios]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_lista_usuarios]  as

	select  username, email, password from usuario
	




GO
/****** Object:  StoredProcedure [dbo].[sp_listado_grupos]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE  proc [dbo].[sp_listado_grupos]  as

	select * from grupo 
	




GO
/****** Object:  StoredProcedure [dbo].[sp_listado_usuarios]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_listado_usuarios]  as

	select  usuario, correo, clave from usuarios
	




GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE  proc [dbo].[sp_login] (@username varchar(20), @password varchar(20)) as

If  Exists (Select * from users (nolock) where username = @username and password=@password )  begin		
	Select * from users (nolock) where username = @username and password=@password 
end


GO
/****** Object:  StoredProcedure [dbo].[sp_login_usuario]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_login_usuario] (@usuario varchar(20), @clave varchar(20)) as

If  Exists (Select * from usuarios (nolock) where usuario = @usuario and clave=@clave )  begin
		insert into auditoria(usuario, fecha,descripcion)
		values(@usuario, getdate(),'Ingreso')
	Select * from usuarios (nolock) where usuario = @usuario and clave=@clave
end


GO
/****** Object:  StoredProcedure [dbo].[sp_save_comment]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  proc [dbo].[sp_save_comment] (@postId int, @name varchar(100), @body varchar(max), @email varchar(100)) as


		Insert into comments(postId,name, body,email)
		values(@postId,@name,@body,@email)
		



GO
/****** Object:  StoredProcedure [dbo].[sp_save_post]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_save_post] (@userid int, @title varchar(100), @body varchar(max)) as

If not Exists (Select * from posts (nolock) where title = @title )  begin
		Insert into posts(userId,title, body)
		values(@userid,@title,@body)
		
end


GO
/****** Object:  StoredProcedure [dbo].[sp_users]    Script Date: 31/03/2023 4:12:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  proc [dbo].[sp_users] (@usuario varchar(20), @clave varchar(20)) as

If  Exists (Select * from usuario (nolock) where username = @usuario and password=@clave )  begin
		insert into auditoria(usuario, fecha,descripcion)
		values(@usuario, getdate(),'Ingreso')
	Select * from usuario (nolock) where username = @usuario and password=@clave
end


GO
