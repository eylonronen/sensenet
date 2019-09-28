﻿//using System;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SenseNet.ContentRepository;
//// ReSharper disable StringLiteralTypo

//namespace SenseNet.ODataTests
//{
//    [TestClass]
//    public class ODataFilterTests : ODataTestBase
//    {
//        [TestMethod]
//        public void OData_Filter_StartsWithEqTrue()
//        {
//            ODataTest(() =>
//            {
//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc/Root",
//                    "?$filter=startswith(Name, 'IM') eq true");

//                var entities = response.Entities;
//                var origIds = Repository.Root.Children
//                    .Where(x => x.Name.StartsWith("IM", StringComparison.OrdinalIgnoreCase))
//                    .Select(f => f.Id)
//                    .ToArray();
//                var ids = entities.Select(e => e.Id).ToArray();

//                Assert.IsTrue(origIds.Length > 0);
//                Assert.AreEqual(0, origIds.Except(ids).Count());
//                Assert.AreEqual(0, ids.Except(origIds).Count());
//            });
//        }

//        [TestMethod]
//        public void OData_Filter_EndsWithEqTrue()
//        {
//            ODataTest(() =>
//            {
//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc/Root",
//                    "?$filter=endswith(Name, 'MS') eq true");

//                var entities = response.Entities;
//                var origIds = Repository.Root.Children
//                    .Where(x => x.Name.EndsWith("MS", StringComparison.OrdinalIgnoreCase))
//                    .Select(f => f.Id)
//                    .ToArray();
//                var ids = entities.Select(e => e.Id).ToArray();

//                Assert.IsTrue(origIds.Length > 0);
//                Assert.AreEqual(0, origIds.Except(ids).Count());
//                Assert.AreEqual(0, ids.Except(origIds).Count());
//            });
//        }

//        [TestMethod]
//        public void OData_Filter_SubstringOfEqTrue()
//        {
//            ODataTest(() =>
//            {
//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc/Root",
//                    "?$filter=substringof('yste', Name) eq true");

//                var entities = response.Entities;
//                var origIds = Repository.Root.Children
//                    .Where(x => x.Name.IndexOf("yste", StringComparison.OrdinalIgnoreCase) >= 0)
//                    .Select(f => f.Id)
//                    .ToArray();
//                var ids = entities.Select(e => e.Id);

//                Assert.IsTrue(origIds.Length > 0);
//                Assert.AreEqual(0, origIds.Except(ids).Count());
//                Assert.AreEqual(0, ids.Except(origIds).Count());
//            });
//        }

//        //        //TODO: Remove inconclusive test result and implement this test.
//        //        //[TestMethod]
//        //        public void OData_Filter_SubstringOfEqListField()
//        //        {
//        //            Assert.Inconclusive("InMemorySchemaWriter.CreatePropertyType is partially implemented.");

//        //            Test(() =>
//        //            {
//        //                CreateTestSite();
//        //                var testRoot = CreateTestRoot("ODataTestRoot");

//        //                var listDef = @"<?xml version='1.0' encoding='utf-8'?>
//        //<ContentListDefinition xmlns='http://schemas.sensenet.com/SenseNet/ContentRepository/ContentListDefinition'>
//        //	<DisplayName>[DisplayName]</DisplayName>
//        //	<Description>[Description]</Description>
//        //	<Icon>[icon.gif]</Icon>
//        //	<Fields>
//        //		<ContentListField name='#CustomField' type='ShortText'>
//        //			<DisplayName>CustomField</DisplayName>
//        //			<Description>CustomField Description</Description>
//        //			<Icon>icon.gif</Icon>
//        //			<Configuration>
//        //				<MaxLength>100</MaxLength>
//        //			</Configuration>
//        //		</ContentListField>
//        //	</Fields>
//        //</ContentListDefinition>
//        //";
//        //                var itemType = "HTMLContent";
//        //                var fieldValue = "qwer asdf yxcv";

//        //                // create list
//        //                var list = new ContentList(testRoot) { Name = Guid.NewGuid().ToString() };
//        //                list.ContentListDefinition = listDef;
//        //                list.AllowedChildTypes = new ContentType[] { ContentType.GetByName(itemType) };
//        //                list.Save();

//        //                // create item
//        //                var item = Content.CreateNew(itemType, list, Guid.NewGuid().ToString());
//        //                item["#CustomField"] = fieldValue;
//        //                item.Save();

//        //                // check expando field accessibility
//        //                item = Content.Load(item.Id);
//        //                Assert.AreEqual(fieldValue, (string)item["#CustomField"]);

//        //                // get base count
//        //                var countByCQ = ContentQuery.Query("#CustomField:*asdf* .AUTOFILTERS:OFF").Count;

//        //                // get ids by SnLinq
//        //                var origIds = Content.All
//        //                    .DisableAutofilters()
//        //                    .Where(x => ((string)x["#CustomField"]).Contains("asdf"))
//        //                    .AsEnumerable()
//        //                    .Select(f => f.Id)
//        //                    .ToArray();
//        //                Assert.IsTrue(origIds.Length > 0);

//        //                // get ids by filter
//        //                var entities1 = ODataGET<ODataEntities>("/OData.svc" + list.Path, "enableautofilters=false&$filter=substringof('asdf', #CustomField) eq true");
//        //                var ids1 = entities1.Select(e => e.Id).ToArray();
//        //                Assert.AreEqual(0, origIds.Except(ids1).Count());
//        //                Assert.AreEqual(0, ids1.Except(origIds).Count());

//        //                // get ids by filter URLencoded
//        //                var entities2 = ODataGET<ODataEntities>("/OData.svc" + list.Path, "enableautofilters=false&$filter=substringof('asdf', %23CustomField) eq true");
//        //                var ids2 = entities2.Select(e => e.Id).ToArray();
//        //                Assert.AreEqual(0, origIds.Except(ids2).Count());
//        //                Assert.AreEqual(0, ids2.Except(origIds).Count());
//        //            });
//        //        }

//        [TestMethod]
//        public void OData_Filter_IsOf()
//        {
//            ODataTest(() =>
//            {
//                InstallCarContentType();
//                var testRoot = CreateTestRoot();

//                var folder = new Folder(testRoot) {Name = Guid.NewGuid().ToString()};
//                folder.Save();
//                var folder1 = new Folder(folder) {Name = "Folder1"};
//                folder1.Save();
//                var folder2 = new Folder(folder) {Name = "Folder2"};
//                folder2.Save();
//                var content = Content.CreateNew("Car", folder, null);
//                content.Save();

//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc" + folder.Path,
//                    "?$filter=isof('Folder')");

//                var entities = response.Entities.ToArray();
//                Assert.AreEqual(2, entities.Length);
//                Assert.AreEqual(folder1.Id, entities[0].Id);
//                Assert.AreEqual(folder2.Id, entities[1].Id);

//                response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc" + folder.Path,
//                    "?&$filter=not isof('Folder')");

//                entities = response.Entities.ToArray();
//                Assert.AreEqual(1, entities.Length);
//                Assert.AreEqual(content.Id, entities[0].Id);
//            });
//        }

//        [TestMethod]
//        public void OData_Filter_IsOfEqTrue()
//        {
//            ODataTest(() =>
//            {
//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc/Root",
//                    "?$filter=isof('Folder') eq true");

//                var origIds = Repository.Root.Children
//                    .Where(x => x.NodeType.IsInstaceOfOrDerivedFrom("Folder"))
//                    .Select(f => f.Id)
//                    .ToArray();

//                var entities = response.Entities.ToArray();
//                var ids = entities.Select(e => e.Id);

//                Assert.IsTrue(origIds.Length > 0);
//                Assert.AreEqual(0, origIds.Except(ids).Count());
//                Assert.AreEqual(0, ids.Except(origIds).Count());
//            });
//        }

//        [TestMethod]
//        public void OData_Filter_ContentField()
//        {
//            ODataTest(() =>
//            {
//                InstallCarContentType();
//                var testRoot = CreateTestRoot();

//                foreach (var item in new[] {"Ferrari", "Porsche", "Ferrari", "Mercedes"})
//                {
//                    var car = Content.CreateNew("Car", testRoot, Guid.NewGuid().ToString());
//                    car["Make"] = item;
//                    car.Save();
//                }

//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc" + testRoot.Path,
//                    "?$filter=Make eq 'Ferrari'&enableautofilters=false");

//                var entities = response.Entities.ToArray();
//                Assert.AreEqual(2, entities.Length);
//            });
//        }


//        [TestMethod]
//        public void OData_Filter_InFolder()
//        {
//            ODataTest(() =>
//            {
//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc/Root/IMS/BuiltIn/Portal",
//                    "?$orderby=Id&$filter=Id lt (9 sub 2)");

//                var entities = response.Entities.ToArray();
//                Assert.AreEqual(2, entities.Length);
//                Assert.AreEqual(1, entities[0].Id);
//                Assert.AreEqual(6, entities[1].Id);
//            });
//        }

//        [TestMethod]
//        public void OData_Filter_IsFolder()
//        {
//            ODataTest(() =>
//            {
//                InstallCarContentType();
//                var testRoot = CreateTestRoot();

//                var folder = new Folder(testRoot) {Name = Guid.NewGuid().ToString()};
//                folder.Save();
//                var folder1 = new Folder(folder) {Name = "Folder1"};
//                folder1.Save();
//                var folder2 = new Folder(folder) {Name = "Folder2"};
//                folder2.Save();

//                var content = Content.CreateNew("Car", folder, null);
//                content.Save();

//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc" + folder.Path,
//                    "?$filter=IsFolder eq true");

//                var entities = response.Entities.ToArray();
//                Assert.AreEqual(2, entities.Length);
//                Assert.AreEqual(folder1.Id, entities[0].Id);
//                Assert.AreEqual(folder2.Id, entities[1].Id);

//                response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc" + folder.Path,
//                    "?$filter=IsFolder eq false");

//                entities = response.Entities.ToArray();
//                Assert.AreEqual(1, entities.Length);
//                Assert.AreEqual(content.Id, entities[0].Id);
//            });
//        }

//        [TestMethod]
//        public void OData_Filter_NamespaceAndMemberChain()
//        {
//            ODataTest(() =>
//            {
//                var name = typeof(ODataFilterTestHelper).FullName;

//                var response = ODataGET<ODataChildrenCollectionResponse>(
//                    "/OData.svc/Root/IMS/BuiltIn/Portal",
//                    $"?$filter={name}/TestValue eq Name");

//                var entities = response.Entities.ToArray();
//                Assert.AreEqual(1, entities.Count());
//                Assert.AreEqual(Group.Administrators.Path, entities.First().Path);
//            });

//        }
//    }
//}