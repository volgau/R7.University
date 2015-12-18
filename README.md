# R7.University

[![Build Status](https://travis-ci.org/roman-yagodin/R7.University.svg?branch=master)](https://travis-ci.org/roman-yagodin/R7.University)
[![Join the chat at https://gitter.im/roman-yagodin/R7.University](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/roman-yagodin/R7.University?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Modules and base library for DNN Platfrom CMS designed to present and manage various assets 
(e.g. divisions, employees, educational programs) for high school educational organization website.

## Dependencies

From version 1.0.4, [R7.ImageHandler](https://github.com/roman-yagodin/R7.ImageHandler) is used
to generate photo thumbnails, and from 1.0.6 also for QR-code generation of contact vCards (but not absolutely require it). 
URL format for thumbnails is `http://www.somewhere.com/imagehandler.ashx?fileid=XXX&width=YYY`, so you can also use or adapt 
[BBImageHandler](http://bbimagehandler.codeplex.com/) or others image generators (though prepare what some things won't work, 
like barcodes with non-latin content, etc.)

## Example of use

*R7.University* modules actively used on the official website of *Volgograd State Agricultural University* http://www.volgau.com. 
On [this page](http://www.volgau.com/LinkClick.aspx?link=284) you could see *Employee*, *EmployeeList* and *Division* module instances. 

## Plans

* Support http://schema.org formats
* Allow manage multiple organizations
* Add organization-level configuration options
