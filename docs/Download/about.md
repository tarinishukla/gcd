---
title: About GCD Software
weight: 2
---

GCD 7 is available as two separate products. The GCD AddIn is for users that possess a copy of ArcGIS. This version includes the ability to visualize GCD layers in ArcMap. The GCD Standalone version is completely independent of ESRI technology and you do not need a copy of ArcGIS to use it. The Standalone is fully functional and the only difference from the AddIn is that the Standalone does not possess any map display.

------

## GCD AddIn for ArcGIS

The following table summarizes the compatibility of GCD versions with ArcGIS:

<table class="tg">
  <tr>
    <th class="tg-0pky">GCD</th>
    <th class="tg-c3ow" colspan="6">ArcGIS</th>
    <th class="tg-0pky">ArcPro</th>
  </tr>
  <tr>
    <th class="tg-0pky"></th>
    <th class="tg-c3ow">10.1</th>
    <th class="tg-c3ow">10.2</th>
    <th class="tg-c3ow">10.3</th>
    <th class="tg-c3ow">10.4</th>
    <th class="tg-c3ow">10.5</th>
    <th class="tg-c3ow">10.6</th>
    <th class="tg-c3ow"></th>
  </tr>
  <tr>
    <td class="tg-0pky">GCD 6</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2716;</td>
  </tr>
  <tr>
    <td class="tg-0pky">GCD 7</td>
    <td class="tg-c3ow">&#x2716;</td>
    <td class="tg-c3ow">&#x2716;</td>
    <td class="tg-c3ow">&#x2716;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2714;</td>
    <td class="tg-c3ow">&#x2716;</td>
  </tr>
</table>



#### Free ArcGIS For Students & Researchers
Most academic institutions have ArcGIS licenses. ESRI makes available [free, fully-fucntional, one-year licenses](http://www.esri.com/landing-pages/software/arcgis/arcgis-desktop-student-trial) to students and researchers. If you want some step-by-step instructions for installing the student version of ArcGIS, see [here](http://gis.joewheaton.org/about/software/arcgis_install). We also recommend that you have [the latest service pack installed](http://resources.arcgis.com/content/patches-and-service-packs?fa=viewPatch&PID=66&MetaID=1843). [More information on student licenses](http://www.joewheaton.org/Home/students-teaching/teaching-materials/gis-help/arcgis-desktop-10---education-edition). 


------
## GCD Standalone

The GCD standalone software is a fully functional version of GCD that can be used to create projects, specify DEM surveys and perform change detections etc. The only difference is that the standalone does not possess any map display. Therefore, if you just want to build a GCD project quickly and don't care about seeing the data displayed in ArcGIS, or you want to use a different GIS package such as [QGIS](https://www.qgis.org/en/site) to visualize GIS datasets, then the standalone might be a good choice for you.

GCD standalone is available as a **64 bit** application only. It therefore should possess better performance than the AddIn version which is only available as a 32 bit version (because ArcGIS 10.x is only available as 32 bit).

GCD standalone is a deployed using Microsoft's ClickOnce technology. This allows the application to be installed in your user profile folder on your computer. You should **not** need administrator privileges to install it. That said, when you click on the above link to download the `setup.exe` installation you will need to read the prompts in your web browser carefully, since most browsers will warn you about downloading executable files.

### Standalone Prerequisites

Both the AddIn and Standalone versions require:

* [Microsoft .Net Framework 4.5.2](https://www.microsoft.com/en-ca/download/details.aspx?id=42642). This should already be installed if you are running Windows 10. It's also required by ArcGIS 10.4.

-----
## Other Notes

Note that currently GCD projects from 6 and 7 are incompatible. i.e. projects created in GCD 6 cannot be opened in GCD 7 and vice versa.

### Running Both Versions
The GCD Standalone and AddIn are independent of each other and installed in separate locations on your machine. Their only shared resource is the FIS Library, and they are able to both access the same `*.gcd` project simultaneously. It is not necessary or required that the Standalone and AddIn are both concurrent versions. For more information, see [here](https://github.com/Riverscapes/gcd/issues/196).

### Additional Suggestions for Tutorials

In order to build surfaces in ArcGIS ([see tutorial]({{ site.baseurl}}/Tutorials/Building_DEMs/building-dems)), you will need the Spatial Analyst & 3D Analyst extensions installed and enable. See [here ](http://gis.joewheaton.org/assignments/labs/lab01/getting-organized-and-oriented/arcgis-10-desktop-orientation/about-the-software#TOC-Using-Extensions)for how to check if you extensions are enabled, or watch this video:

<div class="responsive-embed">
	<iframe width="560" height="315" src="https://www.youtube.com/embed/JgBlCnGco9M?rel=0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</div>



