-- Deshabilitar todas las restricciones de clave externa
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'

-- Realizar la actualizaci�n en la base de datos

-- Habilitar todas las restricciones de clave externa
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'
