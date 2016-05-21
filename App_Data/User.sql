CREATE TABLE IF NOT EXISTS `user`.`praksa` (
  `idPraksa` INT(11) NOT NULL AUTO_INCREMENT,
  `imePraksa` VARCHAR(50) COLLATE utf8_unicode_ci NOT NULL,
  `oibPraksa` VARCHAR(11) COLLATE utf8_unicode_ci NOT NULL,
  `emailPraksa` VARCHAR(100),
  `spolPraksa` VARCHAR(1),
  `adresaPraksa` VARCHAR(100),
  `godinaPraksa` INT(11),
  `drzavaPraksa` VARCHAR(100),
  `posaoPraksa` VARCHAR(100),
  PRIMARY KEY (`idPraksa`)
) ENGINE=MYISAM DEFAULT CHARSET=cp1250 COLLATE=cp1250_croatian_ci;


INSERT INTO `user`.`praksa` (`idPraksa`, `imePraksa`, `oibPraksa`, `emailPraksa`, `spolPraksa`, `adresaPraksa`, `godinaPraksa`, `drzavaPraksa`, `posaoPraksa`) VALUES
(1, 'Mario Marić', '12345678543', 'mmaric@net.hr', 'm', 'Kutina', '1', 'Slovenija', 'programiranje');

SELECT * FROM praksa;

INSERT INTO `user`.`praksa` (`idPraksa`, `imePraksa`, `oibPraksa`, `emailPraksa`, `spolPraksa`, `adresaPraksa`, `godinaPraksa`, `drzavaPraksa`, `posaoPraksa`) VALUES
(2, 'Petar Petrić', '77756785433', 'ppetric@net.hr', 'm', 'Valpovo', '2', 'Češka', 'web dizajn');