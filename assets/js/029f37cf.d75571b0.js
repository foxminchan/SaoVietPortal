"use strict";(self.webpackChunkapi_docs=self.webpackChunkapi_docs||[]).push([[0],{3905:(t,e,l)=>{l.d(e,{Zo:()=>d,kt:()=>b});var n=l(7294);function a(t,e,l){return e in t?Object.defineProperty(t,e,{value:l,enumerable:!0,configurable:!0,writable:!0}):t[e]=l,t}function s(t,e){var l=Object.keys(t);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(t);e&&(n=n.filter((function(e){return Object.getOwnPropertyDescriptor(t,e).enumerable}))),l.push.apply(l,n)}return l}function u(t){for(var e=1;e<arguments.length;e++){var l=null!=arguments[e]?arguments[e]:{};e%2?s(Object(l),!0).forEach((function(e){a(t,e,l[e])})):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(l)):s(Object(l)).forEach((function(e){Object.defineProperty(t,e,Object.getOwnPropertyDescriptor(l,e))}))}return t}function r(t,e){if(null==t)return{};var l,n,a=function(t,e){if(null==t)return{};var l,n,a={},s=Object.keys(t);for(n=0;n<s.length;n++)l=s[n],e.indexOf(l)>=0||(a[l]=t[l]);return a}(t,e);if(Object.getOwnPropertySymbols){var s=Object.getOwnPropertySymbols(t);for(n=0;n<s.length;n++)l=s[n],e.indexOf(l)>=0||Object.prototype.propertyIsEnumerable.call(t,l)&&(a[l]=t[l])}return a}var i=n.createContext({}),k=function(t){var e=n.useContext(i),l=e;return t&&(l="function"==typeof t?t(e):u(u({},e),t)),l},d=function(t){var e=k(t.components);return n.createElement(i.Provider,{value:e},t.children)},o="mdxType",c={inlineCode:"code",wrapper:function(t){var e=t.children;return n.createElement(n.Fragment,{},e)}},h=n.forwardRef((function(t,e){var l=t.components,a=t.mdxType,s=t.originalType,i=t.parentName,d=r(t,["components","mdxType","originalType","parentName"]),o=k(l),h=a,b=o["".concat(i,".").concat(h)]||o[h]||c[h]||s;return l?n.createElement(b,u(u({ref:e},d),{},{components:l})):n.createElement(b,u({ref:e},d))}));function b(t,e){var l=arguments,a=e&&e.mdxType;if("string"==typeof t||a){var s=l.length,u=new Array(s);u[0]=h;var r={};for(var i in e)hasOwnProperty.call(e,i)&&(r[i]=e[i]);r.originalType=t,r[o]="string"==typeof t?t:a,u[1]=r;for(var k=2;k<s;k++)u[k]=l[k];return n.createElement.apply(null,u)}return n.createElement.apply(null,l)}h.displayName="MDXCreateElement"},1178:(t,e,l)=>{l.r(e),l.d(e,{assets:()=>i,contentTitle:()=>u,default:()=>c,frontMatter:()=>s,metadata:()=>r,toc:()=>k});var n=l(7462),a=(l(7294),l(3905));const s={title:"Database Design",description:"Database Design for Sao Viet",sidebar_position:2},u="Database Design",r={unversionedId:"bussiness/database_design",id:"bussiness/database_design",title:"Database Design",description:"Database Design for Sao Viet",source:"@site/docs/bussiness/database_design.md",sourceDirName:"bussiness",slug:"/bussiness/database_design",permalink:"/SaoVietPortal/docs/bussiness/database_design",draft:!1,editUrl:"https://github.com/foxminchan/SaoVietPortal/docs/bussiness/database_design.md",tags:[],version:"current",sidebarPosition:2,frontMatter:{title:"Database Design",description:"Database Design for Sao Viet",sidebar_position:2},sidebar:"tutorialSidebar",previous:{title:"Business Scenario",permalink:"/SaoVietPortal/docs/bussiness/bussiness_context"},next:{title:"Portal service",permalink:"/SaoVietPortal/docs/category/portal-service"}},i={},k=[{value:"Entity Relationship Diagram",id:"entity-relationship-diagram",level:2},{value:"Database Schema",id:"database-schema",level:2},{value:"Table: <code>Student</code>",id:"table-student",level:3},{value:"Table: <code>CourseEnrollment</code>",id:"table-courseenrollment",level:3},{value:"Table: <code>Course</code>",id:"table-course",level:3},{value:"Table: <code>CourseRegistration</code>",id:"table-courseregistration",level:3},{value:"Table: <code>PaymentMethod</code>",id:"table-paymentmethod",level:3},{value:"Table: <code>Class</code>",id:"table-class",level:3},{value:"Table: <code>Branch</code>",id:"table-branch",level:3},{value:"Table: <code>StudentProgress</code>",id:"table-studentprogress",level:3},{value:"Table: <code>Postion</code>",id:"table-postion",level:3},{value:"Table: <code>Staff</code>",id:"table-staff",level:3},{value:"Table: <code>ReceiptExpense</code>",id:"table-receiptexpense",level:3},{value:"ASP.NET Core Identity Schema",id:"aspnet-core-identity-schema",level:2}],d={toc:k},o="wrapper";function c(t){let{components:e,...s}=t;return(0,a.kt)(o,(0,n.Z)({},d,s,{components:e,mdxType:"MDXLayout"}),(0,a.kt)("h1",{id:"database-design"},"Database Design"),(0,a.kt)("p",null,"This section describes the database design for Sao Viet. The database is designed\nusing ",(0,a.kt)("a",{parentName:"p",href:"https://www.microsoft.com/en-us/sql-server/sql-server-downloads"},"MS SQL Server"),". The database is designed to be\nnormalized to 3NF."),(0,a.kt)("h2",{id:"entity-relationship-diagram"},"Entity Relationship Diagram"),(0,a.kt)("p",null,(0,a.kt)("img",{alt:"Entity Relationship Diagram",src:l(7399).Z,width:"1910",height:"1122"})),(0,a.kt)("h2",{id:"database-schema"},"Database Schema"),(0,a.kt)("admonition",{type:"info"},(0,a.kt)("b",{class:"key"},"Attribute")," - An unique identifier for each table."),(0,a.kt)("h3",{id:"table-student"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"Student")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Student is a person who is studying at Sao Viet. A student can enroll in many courses. Each student has a unique student ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each student",(0,a.kt)("br",null),"Length must be ",(0,a.kt)("span",{class:"key"},"10")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"FullName"),(0,a.kt)("td",null,"Nvarchar(50)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"Full name of the student ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"50")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Gender"),(0,a.kt)("td",null,"Bit"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"Sex of the student ",(0,a.kt)("br",null),(0,a.kt)("span",{class:"key"},"True"),": Male ",(0,a.kt)("br",null),(0,a.kt)("span",{class:"key"},"False"),": Female")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Address"),(0,a.kt)("td",null,"Nvarchar(80)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The student's address may include the ward, district, city, country, and other pertinent information. ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"80")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Dob"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",null,"The student's date of birth ",(0,a.kt)("br",null),"Date of birth must be in the past")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Pod"),(0,a.kt)("td",null,"Nvarchar(80)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The student's place of birth may include the ward, district, city, country, and other pertinent information. ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"80")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Occupation"),(0,a.kt)("td",null,"Nvarchar(80)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The student's occupation ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"80")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Social"),(0,a.kt)("td",null,"Nvarchar(max)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The student's social information may include their phone number, email, and social media profiles such as Facebook. This information should be in ",(0,a.kt)("code",{class:"key"},"JSON")," format and must contain the following fields:",(0,a.kt)("ul",null,(0,a.kt)("li",null,(0,a.kt)("code",{class:"key"},"Name"),": The name of the social network"),(0,a.kt)("li",null,(0,a.kt)("code",{class:"key"},"Link"),": The URL to the student's profile on the social network")))))),(0,a.kt)("h3",{id:"table-courseenrollment"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"CourseEnrollment")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Course enrollment is a relationship between a student and a course. A student can enroll in many courses. A course can have many students. Each CourseEnrollment has StudentId and CourseId as primary foreign keys."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," 3NF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"StudentId")),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each student, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Student")," table. Must be 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Student")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"CourseId")),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each course, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Course")," table. Must be 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Course")," table.")))),(0,a.kt)("h3",{id:"table-course"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"Course")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Course is a class that students can enroll in. A course can have many students. Each course has a unique course ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Varchar(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each course",(0,a.kt)("br",null),"Length must be ",(0,a.kt)("span",{class:"key"},"10")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Name"),(0,a.kt)("td",null,"Nvarchar(50)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"Name of the course ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"50")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Description"),(0,a.kt)("td",null,"Nvarchar(max)"),(0,a.kt)("td",null),(0,a.kt)("td",null,"A description of the course")))),(0,a.kt)("h3",{id:"table-courseregistration"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"CourseRegistration")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Course registration is a relationship child of course enrollment. A student can register for many courses. A course can have many students. Each course registration has StudentId and CourseId as foreign keys."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Uniqueidentifier"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique id for each student",(0,a.kt)("br",null),"Generated ",(0,a.kt)("span",{class:"key"},"Guid")," from application")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Status"),(0,a.kt)("td",null,"Nvarchar(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"Indicates the registration status of the student in the course. ",(0,a.kt)("br",null),"Must be one of the following values: ",(0,a.kt)("span",{class:"key"},"Ch\u1ed1t"),", ",(0,a.kt)("span",{class:"key"},"H\u1eb9n"),", ",(0,a.kt)("span",{class:"key"},"Hu\u1ef7"))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"ReregistrationDate"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"The date the student registered for the course.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"AppointmentDate"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The date the student made an appointment for the registration of the course. ",(0,a.kt)("br",null),(0,a.kt)("code",{class:"key"},"AppointmentDate")," \u2265 ",(0,a.kt)("code",{class:"key"},"ReregistrationDate"))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Fee"),(0,a.kt)("td",null,"Float"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The fee the student has to pay for the course. ",(0,a.kt)("br",null),(0,a.kt)("code",{class:"key"},"Fee")," \u2265 0")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Discount"),(0,a.kt)("td",null,"Decimal(4,2)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The discount the student has for the course. ",(0,a.kt)("br",null),(0,a.kt)("code",{class:"key"},"Discount")," \u2265 0 and ",(0,a.kt)("code",{class:"key"},"Discount")," \u2264 100")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"StudentId"),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each student, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Student")," table. Must be 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Student")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"ClassId"),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each class, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Class")," table. Must be 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Class")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"PaymentMethodId"),(0,a.kt)("td",null,"Tinyint"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each payment method, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"PaymentMethod")," table. Must be integer and match an existing value in the ",(0,a.kt)("code",{class:"key"},"PaymentMethod")," table.")))),(0,a.kt)("h3",{id:"table-paymentmethod"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"PaymentMethod")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Payment method is a lookup table that contains the payment methods that students can use to pay for the course. Each payment method has a unique payment method ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Tinyint"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each payment method",(0,a.kt)("br",null),"Must be integer and auto increment")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Name"),(0,a.kt)("td",null,"Nvarchar(50)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"Name of the payment method ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"50")," character long")))),(0,a.kt)("h3",{id:"table-class"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"Class")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Class is a lookup table that contains the classes that students can register for. Each class has a unique class ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each class",(0,a.kt)("br",null),"Length must be ",(0,a.kt)("span",{class:"key"},"10")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"StartDate"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",null,"The date the class starts")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"EndDate"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",null,"The date the class ends ",(0,a.kt)("br",null),(0,a.kt)("code",{class:"key"},"EndDate")," \u2265 ",(0,a.kt)("code",{class:"key"},"StartDate"))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"CourseId"),(0,a.kt)("td",null,"Varchar(10)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each course, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Course")," table. Must be less than 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Course")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"BranchId"),(0,a.kt)("td",null,"Char(8)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each branch, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Branch")," table. Must be 8 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Branch")," table.")))),(0,a.kt)("h3",{id:"table-branch"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"Branch")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Branch is a lookup table that contains the branches that students can register for. Each branch has a unique branch ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Char(8)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each branch",(0,a.kt)("br",null),"Length must be ",(0,a.kt)("span",{class:"key"},"8")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Name"),(0,a.kt)("td",null,"Nvarchar(50)"),(0,a.kt)("td",null),(0,a.kt)("td",null,"Name of the branch ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"50")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Address"),(0,a.kt)("td",null,"Nvarchar(80)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The branch's address may include the ward, district, city, country, and other pertinent information. ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"80")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Phone"),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The branch's phone number ",(0,a.kt)("br",null),"Length must be ",(0,a.kt)("span",{class:"key"},"10")," character long ",(0,a.kt)("br",null),"Regex: ",(0,a.kt)("span",{class:"key"},"\\d{10}"))))),(0,a.kt)("h3",{id:"table-studentprogress"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"StudentProgress")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Student progress is a lookup table that contains the progress of students in a class. Each student progress has a unique student progress ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Uniqueidentifier"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique id for each student progress",(0,a.kt)("br",null),"Generated ",(0,a.kt)("span",{class:"key"},"Guid")," from application")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Name"),(0,a.kt)("td",null,"Nvarchar(80)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"A lesson name of the student progress ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"80")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Content"),(0,a.kt)("td",null,"Nvarchar(max)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The content of the lesson")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"LessonDate"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The date of the lesson ",(0,a.kt)("br",null),(0,a.kt)("code",{class:"key"},"LessonDate")," \u2265 ",(0,a.kt)("code",{class:"key"},"StartDate")," and ",(0,a.kt)("code",{class:"key"},"LessonDate")," \u2264 ",(0,a.kt)("code",{class:"key"},"EndDate")," and must be in the past")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Status"),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The status of the lesson ",(0,a.kt)("br",null),"Must be one of the following values: ",(0,a.kt)("span",{class:"key"},"V\u1eafng h\u1ecdc"),", ",(0,a.kt)("span",{class:"key"},"Mi\u1ec5n h\u1ecdc"),", ",(0,a.kt)("span",{class:"key"},"Ho\xe0n th\xe0nh"))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Ratings"),(0,a.kt)("td",null,"Tinyint"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The rating of the lesson ",(0,a.kt)("br",null),"Must be between ",(0,a.kt)("span",{class:"key"},"0")," and ",(0,a.kt)("span",{class:"key"},"10"))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"TeacherId"),(0,a.kt)("td",null,"Varchar(20)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each teacher, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Teacher")," table. Must be less than 20 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Teacher")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"StudentId"),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each student, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Student")," table. Must be 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Student")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"ClassId"),(0,a.kt)("td",null,"Char(10)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each class, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Class")," table. Must be 10 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Class")," table.")))),(0,a.kt)("h3",{id:"table-postion"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"Postion")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Position is a lookup table that contains the position of teachers. Each position has a unique position ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"Int"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each position",(0,a.kt)("br",null),"Must be integer and auto increment")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Name"),(0,a.kt)("td",null,"Nvarchar(30)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"Name of the position ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"30")," character long")))),(0,a.kt)("h3",{id:"table-staff"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"Staff")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": Staff is a lookup table that contains the information of staffs. Each staff has a unique staff ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"VarChar(20)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each staff",(0,a.kt)("br",null),"Must be less than ",(0,a.kt)("span",{class:"key"},"20")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"FulName"),(0,a.kt)("td",null,"Nvarchar(55)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"Name of the staff ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"55")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Dob"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",null,"The staff's date of birth ",(0,a.kt)("br",null),"Date of birth must be in the past")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Address"),(0,a.kt)("td",null,"Nvarchar(80)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The staff's address may include the ward, district, city, country, and other pertinent information. ",(0,a.kt)("br",null),"Length must be less than ",(0,a.kt)("span",{class:"key"},"80")," character long")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Dsw"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",null,"The staff's date of start working")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"PositionId"),(0,a.kt)("td",null,"Int"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each position, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Position")," table. Must be integer and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Position")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"BranchId"),(0,a.kt)("td",null,"Char(8)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each branch, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Branch")," table. Must be 8 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Branch")," table.")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"ManagerId"),(0,a.kt)("td",null,"Varchar(20)"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",{align:"justify"},"An unique identifier for each manager, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Manager")," table. Must be less than 20 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Manager")," table.")))),(0,a.kt)("h3",{id:"table-receiptexpense"},"Table: ",(0,a.kt)("inlineCode",{parentName:"h3"},"ReceiptExpense")),(0,a.kt)("p",{align:"justify"},(0,a.kt)("b",null,"Description"),": ReceiptExpense is a lookup table that contains the information of receipt expenses. Each receipt expense has a unique receipt expense ID."),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Normal form:")," BCNF"),(0,a.kt)("table",null,(0,a.kt)("thead",null,(0,a.kt)("tr",null,(0,a.kt)("th",null,"Column"),(0,a.kt)("th",null,"Type"),(0,a.kt)("th",null,"Required"),(0,a.kt)("th",null,"Description"))),(0,a.kt)("tbody",null,(0,a.kt)("tr",null,(0,a.kt)("td",null,(0,a.kt)("b",{class:"key"},"Id")),(0,a.kt)("td",null,"UniqueIdentifier"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"An unique id for each receipt expense",(0,a.kt)("br",null),"Generated ",(0,a.kt)("span",{class:"key"},"Guid")," from application")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Type"),(0,a.kt)("td",null,"Bit"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,(0,a.kt)("ul",null,(0,a.kt)("li",null,(0,a.kt)("span",{class:"key"},"0"),": Expense"),(0,a.kt)("li",null,(0,a.kt)("span",{class:"key"},"1"),": Receipt")))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null,"Date"),(0,a.kt)("td",null),(0,a.kt)("td",null,"The date of the receipt expense")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Amount"),(0,a.kt)("td",null,"Float"),(0,a.kt)("td",{align:"center"},(0,a.kt)("label",null,(0,a.kt)("input",{type:"checkbox",checked:!0,disabled:!0}))),(0,a.kt)("td",null,"The amount of the receipt expense ",(0,a.kt)("br",null),"Must be greater than ",(0,a.kt)("span",{class:"key"},"0"))),(0,a.kt)("tr",null,(0,a.kt)("td",null,"Note"),(0,a.kt)("td",null,"Nvarchar(max)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"The note of the receipt expense")),(0,a.kt)("tr",null,(0,a.kt)("td",null,"BranchId"),(0,a.kt)("td",null,"Char(8)"),(0,a.kt)("td",null),(0,a.kt)("td",{align:"justify"},"An unique identifier for each branch, used as a foreign key to reference the ",(0,a.kt)("code",{class:"key"},"Branch")," table. Must be 8 characters long and match an existing value in the ",(0,a.kt)("code",{class:"key"},"Branch")," table.")))),(0,a.kt)("h2",{id:"aspnet-core-identity-schema"},"ASP.NET Core Identity Schema"),(0,a.kt)("p",{align:"justify"},"ASP.NET Core Identity is an API, which provides a framework for implementing authentication and authorization in .NET Core applications. It is an open-source replacement for the previous ASP.NET Membership system. ASP.NET Core Identity allows you to add login features to your application and makes it easy to customize data about the logged-in user."),(0,a.kt)("p",{align:"justify"},"ASP.NET Core Identity uses a SQL Server database to store user names, passwords, and profile data. The schema for the database is automatically created when you create the project. The schema is based on the ASP.NET Core Identity Entity Framework Core Code First default schema. The schema is designed to support the default ASP.NET Core Identity UI, which you can scaffold into your project."),(0,a.kt)("p",{align:"justify"},"The ASP.NET Core Identity schema is designed to support the default ASP.NET Core Identity UI, which you can scaffold into your project. The schema is based on the ASP.NET Core Identity Entity Framework Core Code First default schema. The schema is automatically created when you create the project."),(0,a.kt)("p",{align:"justify"},"Learn more about ASP.NET Core Identity schema at ",(0,a.kt)("a",{href:"https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0#aspnet-core-identity-schema"},"ASP.NET Core Identity schema"),"."))}c.isMDXComponent=!0},7399:(t,e,l)=>{l.d(e,{Z:()=>n});const n=l.p+"assets/images/SaoVietErDiagram-3d4cbf9d9b01189803d4c99fcbbbfdb5.png"}}]);