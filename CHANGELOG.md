## [1.5.40] - 2017-04-28
### Added
- Added Filter based on Job Title setup in User Role. 
- 

### Changed
- Display employees by User Role in Employee list.
- Changed masking field setup from based on Employee Type to Is Masking in User Role.


## [1.5.32] - 2017-04-13
### Added
- Automatically contract termination in Job Contract (Fix Term contract only).
- Automatically inactive employee compliance in Compliance.
- Exported to excel for data that failed to be imported into database at the time of importing Job Contract from excel.

### Changed
- Issue #89 Changed error messages from Exception to custom message.
- Issue #149 Removed field masking from add and edit an employee form.

### Fixed
- Fixes issue #136 Compliance item validation. 
- Fixes issue #148 Failed export to xls in employee list without filtering first.


## [1.5.31] - 2017-04-06
### Added
- Added export to PDF for Company Setup (Administration) report.

### Changed
- Changed masking field setup from User Management to Role Management.


## [1.5.30] - 2017-03-31
### Added
- Added Company Setup (Administration) report.
- Added for ACA report (detail data display in the table).
- Added Export to Excel for Payroll report.
- Added Export to Excel for Benefit report.
- Added Export to Excel for Compliance report.
- Added Export to Excel for ACA report.
- Added Export to PDF for Company report.
- Added loading bar.

### Fixed
- Fixed ACA eligibility calculation. 


## [1.5.21] - 2017-03-24
### Fixed
- Issue #93 Benefit > Plan Year > Detail - Not display properly(Not shown popup)


## [1.5.20] - 2017-03-22
### Added
- Export to XLS for Employee List Report.
- Compliance report with advanced filter.
- Benefit report with advanced filter.
- Payroll report with advanced filter.
- Master Data report with advanced filter.
- SMP overlappng validation in add/edit a Plan Year.
- Employee WOTC Summary report in employee detail.

### Changed
- Issue #79 Benefit Plan - Header changed "Minumum Value" be "Minimum Value".

### Fixed
- Issue #81 Warning message should not appeared when user follow step properly.
- Issue #80 User can choose rate option by tick, and display properly.
- Issue #87 File Management, Import Payroll File - validation below filed still appeared even already choose file to upload
- Issue #88 File Management , Vesta Service log - warning message appeared even already choose data to upload