USE [Auth]
GO

/****** Object:  Table [dbo].[tbl_Pwa_Security_Users]    Script Date: 06/06/2025 05:20:57 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_Pwa_Security_Users](
	[intUserId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[strDni] [nvarchar](50) NOT NULL,
	[strName] [nvarchar](50) NOT NULL,
	[strLastName] [nvarchar](50) NOT NULL,
	[strPassword] [nvarchar](50) NOT NULL,
	[strEmail] [nvarchar](50) NOT NULL,
	[strPhone] [nvarchar](50) NULL,
	[strBirthDate] [nvarchar](50) NULL,
	[bitState] [nchar](10) NULL,
 CONSTRAINT [PK_tbl_Pwa_Security_Users] PRIMARY KEY CLUSTERED 
(
	[intUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

********************************************************************************************************************
********************************************************************************************************************
********************************************************************************************************************

USE [Auth]
GO
/****** Object:  StoredProcedure [dbo].[sp_Pwa_Add_Users]    Script Date: 06/06/2025 05:18:20 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Aleida Quiroga
-- Create date: 2025-06-03 -- Se completó la fecha de creación
-- Description: Sistema Amigo - Stored Procedure para añadir usuarios de PWA.
-- exec [sp_Pwa_Add_Users] '234563', 'Lu', 'Luu', '0123450', 'lu@gmail.com', '2345678', '12/12/2003', '1'
-- =============================================
ALTER PROCEDURE [dbo].[sp_Pwa_Add_Users]
    @strDni NVARCHAR(50),
    @strName NVARCHAR(50),
    @strLastName NVARCHAR(50),
    @strPassword NVARCHAR(50), 
    @strEmail NVARCHAR(50),
    @strPhone NVARCHAR(50) = NULL,      
    @strBirthDate DATE = NULL,   
    @bitState BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT

    -- Verificar si el usuario ya existe por DNI o Email
    SELECT @UserId = intUserId 
    FROM [dbo].tbl_pwa_Security_Users 
    WHERE strDni = @strDni OR strEmail = @strEmail

    -- Si no existe, lo insertamos
    IF @UserId IS NULL
    BEGIN
        INSERT INTO [dbo].tbl_pwa_Security_Users 
            (strDni, strName, strLastName, strPassword, strEmail, strPhone, strBirthDate, bitState)
        VALUES 
            (@strDni, @strName, @strLastName, @strPassword, @strEmail, @strPhone, @strBirthDate, @bitState)

        SET @UserId = SCOPE_IDENTITY()
    END

    -- Devolver los datos del usuario (nuevo o existente)
    SELECT 
        intUserId,
        strDni,
        strName,
        strLastName,
        strPassword,
        strEmail,
        strPhone,
        strBirthDate
    FROM [dbo].tbl_pwa_Security_Users
    WHERE intUserId = @UserId
END

********************************************************************************************************************
********************************************************************************************************************
********************************************************************************************************************


USE [Auth]
GO
/****** Object:  StoredProcedure [dbo].[sp_Pwa_Security_Login]    Script Date: 06/06/2025 05:17:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Aleida Quiroga
-- Create date: 
-- Description:	Sistema Amigo
-- exec [sp_Pwa_Security_Login]'string','string'
-- =============================================
ALTER PROCEDURE [dbo].[sp_Pwa_Security_Login]
    @strEmail VARCHAR(50),
	@strPassword VARCHAR(50)
AS
BEGIN
  SELECT intUserId, strDni, strName, strLastName, strPassword, strEmail, strPhone, strBirthDate 
  FROM [dbo].[tbl_Pwa_Security_Users]
  WHERE strEmail = @strEmail AND strPassword = @strPassword AND bitState = 1

END

********************************************************************************************************************
********************************************************************************************************************
********************************************************************************************************************

USE [Auth]
GO
/****** Object:  StoredProcedure [dbo].[sp_Pwa_Security_Get_Users]    Script Date: 06/06/2025 05:19:06 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Aleida Quiroga
-- Create date: 
-- Description: Sistema Amigo
-- exec [sp_Pwa_Security_Get_Login] 
-- =============================================
ALTER PROCEDURE [dbo].[sp_Pwa_Security_Get_Users]			

AS
BEGIN
    SET NOCOUNT ON;
		SELECT 
		intUserId,
        strDni,
        strName,
        strLastName,
        strPassword,
        strEmail,
        strPhone,
        strBirthDate
    FROM [dbo].tbl_pwa_Security_Users
	END

********************************************************************************************************************
********************************************************************************************************************
********************************************************************************************************************
USE [Auth]
GO
/****** Object:  StoredProcedure [dbo].[sp_Pwa_Security_ChangePassword]    Script Date: 06/06/2025 05:19:31 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Aleida Quiroga
-- Create date: 
-- Description: Sistema Amigo
-- exec [sp_Pwa_Security_ChangePassword]'alq@gmail.com','12345678', '123456'
-- =============================================
ALTER PROCEDURE [dbo].[sp_Pwa_Security_ChangePassword]
    @strEmail VARCHAR(50),
    @strCurrentPassword VARCHAR(50),
    @strNewPassword VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @intUserId INT;

    -- Verificar credenciales
    SELECT @intUserId = intUserId
    FROM [dbo].[tbl_Pwa_Security_Users]
    WHERE strEmail = @strEmail
      AND strPassword = @strCurrentPassword
      AND bitState = 1;

    IF @intUserId IS NOT NULL
    BEGIN
        -- Actualizar contraseña
        UPDATE [dbo].[tbl_Pwa_Security_Users]
        SET strPassword = @strNewPassword
        WHERE intUserId = @intUserId;

        -- Devolver datos del usuario (sin contraseña)
        SELECT
            intUserId,
            strDni,
            strName,
            strLastName,
			strPassword,
            strEmail,
            strPhone,
            strBirthDate

        FROM [dbo].[tbl_Pwa_Security_Users]
        WHERE intUserId = @intUserId;
    END
    -- Si no coincide, no devuelve nada → dr.ReadAsync() = false
END

********************************************************************************************************************
********************************************************************************************************************
********************************************************************************************************************
USE [Auth]
GO
/****** Object:  StoredProcedure [dbo].[sp_Pwa_Delete_User]    Script Date: 06/06/2025 05:19:52 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Aleida Quiroga
-- Create date: 
-- Description: Sistema Amigo
-- exec [sp_Pwa_Delete_User] 5
-- =============================================
ALTER PROCEDURE [dbo].[sp_Pwa_Delete_User]
    @intUserId NUMERIC (18,0)
AS
BEGIN
    SET NOCOUNT ON;
	IF EXISTS(
	SELECT 1 
	FROM [dbo].[tbl_Pwa_Security_Users]
	WHERE intUserId =  @intUserId
	)

	BEGIN
	DELETE  FROM [dbo].[tbl_Pwa_Security_Users]
	WHERE intUserId =  @intUserId
	END
   END