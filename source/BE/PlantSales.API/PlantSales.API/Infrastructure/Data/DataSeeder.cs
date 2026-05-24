using Microsoft.EntityFrameworkCore;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Domain.Enums;

namespace PlantSales.API.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.TreeStyles.AnyAsync())
            return; // Already seeded

        // Seed TreeStyles
        var treeStyles = new List<TreeStyle>
        {
            new() { Name = "Thẳng đứng", Slug = "thang-dung", Description = "Dáng cây thẳng đứng, cân đối" },
            new() { Name = "Nghiêng", Slug = "nghieng", Description = "Dáng cây nghiêng về một bên" },
            new() { Name = "Nằm ngang", Slug = "nam-ngang", Description = "Dáng cây nằm ngang, nhánh chính nằm ngang" },
            new() { Name = "Bụi", Slug = "bui", Description = "Dáng cây nhiều thân, tạo thành bụi" },
            new() { Name = "Rừng", Slug = "rung", Description = "Dáng cây rừng, nhiều cây nhỏ tạo thành rừng" }
        };
        await db.TreeStyles.AddRangeAsync(treeStyles);
        await db.SaveChangesAsync();

        // Seed PotShapes
        var potShapes = new List<PotShape>
        {
            new() { Name = "Tròn", Description = "Chậu tròn cổ điển" },
            new() { Name = "Vuông", Description = "Chậu vuông hiện đại" },
            new() { Name = "Chữ nhật", Description = "Chậu chữ nhật dài" },
            new() { Name = "Bầu dục", Description = "Chậu hình bầu dục" }
        };
        await db.PotShapes.AddRangeAsync(potShapes);
        await db.SaveChangesAsync();

        // Seed PotSizes
        var potSizes = new List<PotSize>
        {
            new() { SizeCm = 15, Description = "15cm - Cây nhỏ" },
            new() { SizeCm = 20, Description = "20cm - Cây vừa" },
            new() { SizeCm = 25, Description = "25cm - Cây vừa" },
            new() { SizeCm = 30, Description = "30cm - Cây lớn" },
            new() { SizeCm = 35, Description = "35cm - Cây lớn" },
            new() { SizeCm = 40, Description = "40cm - Cây rất lớn" }
        };
        await db.PotSizes.AddRangeAsync(potSizes);
        await db.SaveChangesAsync();

        // Reload IDs
        var styles = await db.TreeStyles.ToListAsync();
        var shapes = await db.PotShapes.ToListAsync();
        var sizes = await db.PotSizes.ToListAsync();

        // Seed BonsaiTrees
        var trees = new List<BonsaiTree>
        {
            new()
            {
                Code = "MAI001",
                Name = "Cây mai vàng thẳng đứng",
                Slug = "cay-mai-vang-thang-dung",
                Description = "Cây mai vàng đẹp, dáng thẳng đứng, hoa vàng rực rỡ vào tết",
                ShortDescription = "Mai vàng thẳng đứng, 20 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Mai+Vang",
                ImportPrice = 5_000_000,
                ActualPrice = 8_000_000,
                DisplayPrice = "8 triệu",
                TreeStyleId = styles[0].Id, // Thẳng đứng
                PotShapeId = shapes[0].Id,  // Tròn
                PotSizeId = sizes[2].Id,    // 25cm
                TrunkCircumference = 15m,
                CanopyWidth = 25m,
                CanopyHeight = 45m,
                FruitCount = 0,
                CareInstruction = "Tưới nước mỗi ngày, đặt nơi có ánh sáng gián tiếp",
                ViewCount = 156,
                IsFeatured = true,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                Code = "SON001",
                Name = "Cây tùng lúc nằm ngang",
                Slug = "cay-tung-loc-nam-ngang",
                Description = "Tùng lúc đỏ, dáng nằm ngang, rất đẹp và hiếm có",
                ShortDescription = "Tùng lúc đỏ nằm ngang, 30 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Tung+Loc",
                ImportPrice = 8_000_000,
                ActualPrice = 15_000_000,
                DisplayPrice = "15 triệu",
                TreeStyleId = styles[2].Id, // Nằm ngang
                PotShapeId = shapes[2].Id,  // Chữ nhật
                PotSizeId = sizes[3].Id,    // 30cm
                TrunkCircumference = 20m,
                CanopyWidth = 50m,
                CanopyHeight = 30m,
                FruitCount = 0,
                CareInstruction = "Tưới nước đều, tránh nước ngọt, cần nơi thoáng khí",
                ViewCount = 234,
                IsFeatured = true,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-20)
            },
            new()
            {
                Code = "SAO001",
                Name = "Cây sao vàng bụi",
                Slug = "cay-sao-vang-bui",
                Description = "Sao vàng kiểu bụi, nhiều thân, rất độc đáo",
                ShortDescription = "Sao vàng bụi, 15 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Sao+Vang",
                ImportPrice = 3_000_000,
                ActualPrice = 5_500_000,
                DisplayPrice = "5.5 triệu",
                TreeStyleId = styles[3].Id, // Bụi
                PotShapeId = shapes[0].Id,  // Tròn
                PotSizeId = sizes[1].Id,    // 20cm
                TrunkCircumference = 8m,
                CanopyWidth = 30m,
                CanopyHeight = 20m,
                FruitCount = 0,
                CareInstruction = "Tưới nước vừa phải, thích hợp cho người mới bắt đầu",
                ViewCount = 89,
                IsFeatured = false,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-15)
            },
            new()
            {
                Code = "SUNG001",
                Name = "Cây sung rừng cổ thụ",
                Slug = "cay-sung-rung-co-thu",
                Description = "Sung kiểu rừng, 5 thân nhỏ tạo thành rừng nhỏ, rất hiếm",
                ShortDescription = "Sung rừng 5 thân, 25 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Sung+Rung",
                ImportPrice = 12_000_000,
                ActualPrice = 22_000_000,
                DisplayPrice = "22 triệu",
                TreeStyleId = styles[4].Id, // Rừng
                PotShapeId = shapes[2].Id,  // Chữ nhật
                PotSizeId = sizes[4].Id,    // 35cm
                TrunkCircumference = 25m,
                CanopyWidth = 60m,
                CanopyHeight = 50m,
                FruitCount = 50,
                CareInstruction = "Cần chăm sóc kỹ, tưới nước đều, cắt tỉa định kỳ",
                ViewCount = 456,
                IsFeatured = true,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                Code = "LINH001",
                Name = "Cây linh sam thẳng nghiêng",
                Slug = "cay-linh-sam-thang-nghieng",
                Description = "Linh sam xanh tươi, dáng thẳng nghiêng, rất sống động",
                ShortDescription = "Linh sam xanh, 12 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Linh+Sam",
                ImportPrice = 2_500_000,
                ActualPrice = 4_200_000,
                DisplayPrice = "4.2 triệu",
                TreeStyleId = styles[1].Id, // Nghiêng
                PotShapeId = shapes[1].Id,  // Vuông
                PotSizeId = sizes[0].Id,    // 15cm
                TrunkCircumference = 6m,
                CanopyWidth = 20m,
                CanopyHeight = 25m,
                FruitCount = 0,
                CareInstruction = "Thích nước, tưới nước thường xuyên, để nơi mát mẻ",
                ViewCount = 120,
                IsFeatured = false,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-5)
            },
            new()
            {
                Code = "DUONG001",
                Name = "Cây đương tùng lúc",
                Slug = "cay-duong-tung-loc",
                Description = "Tùng lúc đương, dáng thẳng đứng, rất thẳng thắn",
                ShortDescription = "Tùng lúc đương, 18 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Duong+Tung",
                ImportPrice = 6_000_000,
                ActualPrice = 10_500_000,
                DisplayPrice = "10.5 triệu",
                TreeStyleId = styles[0].Id, // Thẳng đứng
                PotShapeId = shapes[3].Id,  // Bầu dục
                PotSizeId = sizes[2].Id,    // 25cm
                TrunkCircumference = 12m,
                CanopyWidth = 20m,
                CanopyHeight = 40m,
                FruitCount = 0,
                CareInstruction = "Tưới nước đều, tránh úng nước, cần nơi sáng",
                ViewCount = 198,
                IsFeatured = false,
                IsSold = false,
                Status = TreeStatusEnum.Reserved,
                PublishedAt = DateTime.UtcNow.AddDays(-8)
            },
            new()
            {
                Code = "HOA001",
                Name = "Cây hoa anh đào bụi",
                Slug = "cay-hoa-anh-dao-bui",
                Description = "Anh đào kiểu bụi, hoa hồng xinh đẹp, nở hoa vào mùa xuân",
                ShortDescription = "Hoa anh đào bụi, 10 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Hoa+Anh+Dao",
                ImportPrice = 4_000_000,
                ActualPrice = 7_000_000,
                DisplayPrice = "7 triệu",
                TreeStyleId = styles[3].Id, // Bụi
                PotShapeId = shapes[0].Id,  // Tròn
                PotSizeId = sizes[1].Id,    // 20cm
                TrunkCircumference = 10m,
                CanopyWidth = 25m,
                CanopyHeight = 25m,
                FruitCount = 0,
                CareInstruction = "Tưới nước mỗi ngày, đặt nơi có ánh sáng, cắt tỉa sau khi hoa tàn",
                ViewCount = 245,
                IsFeatured = false,
                IsSold = false,
                Status = TreeStatusEnum.Deposited,
                PublishedAt = DateTime.UtcNow.AddDays(-3)
            },
            new()
            {
                Code = "PHUONG001",
                Name = "Cây phương nữ nằm ngang",
                Slug = "cay-phuong-nu-nam-ngang",
                Description = "Phương nữ kiểu nằm ngang, rất độc đáo và quý hiếm",
                ShortDescription = "Phương nữ nằm ngang, 35 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Phuong+Nu",
                ImportPrice = 15_000_000,
                ActualPrice = 28_000_000,
                DisplayPrice = "28 triệu",
                TreeStyleId = styles[2].Id, // Nằm ngang
                PotShapeId = shapes[2].Id,  // Chữ nhật
                PotSizeId = sizes[5].Id,    // 40cm
                TrunkCircumference = 35m,
                CanopyWidth = 80m,
                CanopyHeight = 40m,
                FruitCount = 100,
                CareInstruction = "Cây cổ thụ, cần chăm sóc kỹ lưỡng, tưới nước đều, định kỳ cắt tỉa",
                ViewCount = 678,
                IsFeatured = true,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-2)
            },
            new()
            {
                Code = "CHUM001",
                Name = "Cây chùm ngây rừng",
                Slug = "cay-chum-ngay-rung",
                Description = "Chùm ngây kiểu rừng, nhiều thân, hoa trắng xinh đẹp",
                ShortDescription = "Chùm ngây rừng, 8 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Chum+Ngay",
                ImportPrice = 1_800_000,
                ActualPrice = 3_200_000,
                DisplayPrice = "3.2 triệu",
                TreeStyleId = styles[4].Id, // Rừng
                PotShapeId = shapes[0].Id,  // Tròn
                PotSizeId = sizes[0].Id,    // 15cm
                TrunkCircumference = 4m,
                CanopyWidth = 25m,
                CanopyHeight = 20m,
                FruitCount = 0,
                CareInstruction = "Tưới nước vừa phải, thích hợp cho người mới học bonsai",
                ViewCount = 67,
                IsFeatured = false,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-12)
            },
            new()
            {
                Code = "HON001",
                Name = "Cây hồn nước thẳng đứng",
                Slug = "cay-hon-nuoc-thang-dung",
                Description = "Hồn nước xanh tươi, dáng thẳng đứng, rất vui nhộn",
                ShortDescription = "Hồn nước xanh, 9 năm tuổi",
                ThumbnailUrl = "https://placehold.co/400x400?text=Hon+Nuoc",
                ImportPrice = 2_200_000,
                ActualPrice = 3_800_000,
                DisplayPrice = "3.8 triệu",
                TreeStyleId = styles[0].Id, // Thẳng đứng
                PotShapeId = shapes[1].Id,  // Vuông
                PotSizeId = sizes[1].Id,    // 20cm
                TrunkCircumference = 7m,
                CanopyWidth = 18m,
                CanopyHeight = 30m,
                FruitCount = 0,
                CareInstruction = "Tưới nước mỗi ngày, đặt nơi thoáng khí, tránh nắng trực tiếp",
                ViewCount = 145,
                IsFeatured = false,
                IsSold = false,
                Status = TreeStatusEnum.Available,
                PublishedAt = DateTime.UtcNow.AddDays(-7)
            }
        };
        await db.BonsaiTrees.AddRangeAsync(trees);
        await db.SaveChangesAsync();

        // Reload tree IDs
        var treesWithIds = await db.BonsaiTrees.ToListAsync();

        // Seed BonsaiImages
        var images = new List<BonsaiImage>();
        foreach (var tree in treesWithIds)
        {
            images.Add(new()
            {
                BonsaiTreeId = tree.Id,
                ImageUrl = $"https://placehold.co/600x400?text={tree.Name.Replace(" ", "+")}+1",
                IsThumbnail = true,
                SortOrder = 1
            });
            images.Add(new()
            {
                BonsaiTreeId = tree.Id,
                ImageUrl = $"https://placehold.co/600x400?text={tree.Name.Replace(" ", "+")}+2",
                IsThumbnail = false,
                SortOrder = 2
            });
        }
        await db.BonsaiImages.AddRangeAsync(images);
        await db.SaveChangesAsync();
    }
}
