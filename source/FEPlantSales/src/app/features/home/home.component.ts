import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PlantService } from '../../core/services/api/plant.service';
import { PlantListDto } from '../../core/models/plant.model';
import { PlantCardComponent } from '../../shared/components/plant-card/plant-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, PlantCardComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
  currentSlide = 0;
  currentTestimonial = 0;
  isAnimated = false;
  private slideTimer?: ReturnType<typeof setInterval>;
  private testimonialTimer?: ReturnType<typeof setInterval>;
  private animationTimer?: ReturnType<typeof setTimeout>;

  subscribeEmail = '';
  featuredPlants: PlantListDto[] = [];

  heroSlides = [
    {
      image: 'img/bg-img/1.jpg',
      title: 'Cây cảnh đẹp, không gian xanh cho ngôi nhà của bạn',
      description: 'Chúng tôi cung cấp các loại cây cảnh bonsai, cây phong thủy, cây văn phòng chất lượng cao, mang lại vẻ đẹp tự nhiên cho không gian sống của bạn.',
    },
    {
      image: 'img/bg-img/2.jpg',
      title: 'Bộ sưu tập bonsai độc đáo, tinh tế',
      description: 'Khám phá bộ sưu tập bonsai được chăm sóc kỳ công, phù hợp làm quà tặng ý nghĩa hoặc trang trí không gian sống và văn phòng.',
    }
  ];

  services = [
    {
      icon: 'img/core-img/s1.png',
      title: 'Chăm sóc cây cảnh',
      description: 'Dịch vụ chăm sóc cây cảnh tận nơi, đảm bảo cây luôn xanh tốt và phát triển khỏe mạnh trong không gian của bạn.'
    },
    {
      icon: 'img/core-img/s2.png',
      title: 'Tư vấn phong thủy',
      description: 'Tư vấn chọn cây phù hợp phong thủy, mang lại tài lộc và may mắn cho gia đình và doanh nghiệp của bạn.'
    },
    {
      icon: 'img/core-img/s3.png',
      title: 'Giao hàng tận nhà',
      description: 'Đóng gói cẩn thận, giao hàng nhanh chóng tận nơi, đảm bảo cây đến tay bạn trong tình trạng tốt nhất.'
    }
  ];

  progressBars = [
    { label: 'Cây văn phòng', value: 80 },
    { label: 'Bonsai nghệ thuật', value: 90 },
    { label: 'Thiết kế sân vườn', value: 75 },
    { label: 'Chăm sóc cây', value: 85 }
  ];

  benefits = [
    { icon: 'img/core-img/b1.png', title: 'Chất lượng cao', desc: 'Cây được chọn lọc kỹ càng, đảm bảo chất lượng và tính thẩm mỹ trước khi đến tay khách hàng.' },
    { icon: 'img/core-img/b2.png', title: 'Dịch vụ hoàn hảo', desc: 'Đội ngũ tư vấn nhiệt tình, hỗ trợ 24/7, giải đáp mọi thắc mắc của khách hàng nhanh chóng.' },
    { icon: 'img/core-img/b3.png', title: '100% Tự nhiên', desc: 'Toàn bộ cây đều được trồng và chăm sóc tự nhiên, không sử dụng hóa chất độc hại.' },
    { icon: 'img/core-img/b4.png', title: 'Thân thiện môi trường', desc: 'Cam kết bảo vệ môi trường, sử dụng vật liệu tái chế và phương pháp trồng cây bền vững.' }
  ];

  portfolioItems = [
    { image: 'img/bg-img/16.jpg', title: 'Bonsai Văn phòng', category: 'Bonsai' },
    { image: 'img/bg-img/17.jpg', title: 'Cây sân vườn', category: 'Sân vườn' },
    { image: 'img/bg-img/18.jpg', title: 'Trang trí nội thất', category: 'Nội thất' },
    { image: 'img/bg-img/19.jpg', title: 'Thiết kế cảnh quan', category: 'Cảnh quan' },
    { image: 'img/bg-img/20.jpg', title: 'Cây phong thủy', category: 'Phong thủy' },
    { image: 'img/bg-img/21.jpg', title: 'Vườn mini', category: 'Sân vườn' },
    { image: 'img/bg-img/22.jpg', title: 'Bộ sưu tập đặc biệt', category: 'Bonsai' }
  ];

  testimonials = [
    {
      image: 'img/bg-img/13.jpg',
      quote: '"Tôi rất hài lòng với dịch vụ tại đây. Cây được đóng gói cẩn thận, giao hàng nhanh và cây rất đẹp đúng như mô tả. Sẽ tiếp tục ủng hộ shop!"',
      name: 'Nguyễn Thị Mai',
      role: 'Khách hàng'
    },
    {
      image: 'img/bg-img/14.jpg',
      quote: '"Shop có rất nhiều loại cây đẹp, đặc biệt là bộ sưu tập bonsai rất độc đáo. Nhân viên tư vấn nhiệt tình, giá cả hợp lý. Highly recommend!"',
      name: 'Trần Văn Hùng',
      role: 'Khách hàng thân thiết'
    },
    {
      image: 'img/bg-img/15.jpg',
      quote: '"Mua tặng bạn bè và ai cũng thích. Cây khỏe mạnh, chậu đẹp, đóng gói chuyên nghiệp. Đây là lần thứ 3 tôi mua ở đây và lần nào cũng hài lòng."',
      name: 'Lê Thị Hoa',
      role: 'Khách hàng'
    }
  ];

  blogPosts = [
    {
      image: 'img/bg-img/6.jpg',
      title: '5 loại cây cảnh phù hợp nhất cho văn phòng giúp tăng năng suất làm việc',
      date: '15 Tháng 5, 2025',
      author: 'Đức Hiểu',
      excerpt: 'Cây xanh trong văn phòng không chỉ làm đẹp không gian mà còn giúp giảm stress, tăng tập trung và cải thiện chất lượng không khí.'
    },
    {
      image: 'img/bg-img/7.jpg',
      title: 'Hướng dẫn chăm sóc cây bonsai cho người mới bắt đầu - từng bước chi tiết',
      date: '10 Tháng 5, 2025',
      author: 'Đức Hiểu',
      excerpt: 'Bonsai là nghệ thuật trồng cây trong chậu nhỏ. Bài viết này sẽ hướng dẫn bạn những bước cơ bản để chăm sóc bonsai hiệu quả.'
    },
    {
      image: 'img/bg-img/8.jpg',
      title: 'Top 10 cây phong thủy mang lại tài lộc và may mắn cho gia đình bạn',
      date: '5 Tháng 5, 2025',
      author: 'Đức Hiểu',
      excerpt: 'Theo phong thủy, một số loại cây có khả năng thu hút năng lượng tích cực, mang lại tài lộc và may mắn cho gia chủ.'
    }
  ];

  constructor(private readonly plantService: PlantService) {}

  ngOnInit(): void {
    this.startHeroSlider();
    this.startTestimonialSlider();
    this.loadFeaturedPlants();
    this.animationTimer = setTimeout(() => { this.isAnimated = true; }, 400);
  }

  ngOnDestroy(): void {
    clearInterval(this.slideTimer);
    clearInterval(this.testimonialTimer);
    clearTimeout(this.animationTimer);
  }

  private startHeroSlider(): void {
    this.slideTimer = setInterval(() => {
      this.currentSlide = (this.currentSlide + 1) % this.heroSlides.length;
    }, 5000);
  }

  private startTestimonialSlider(): void {
    this.testimonialTimer = setInterval(() => {
      this.currentTestimonial = (this.currentTestimonial + 1) % this.testimonials.length;
    }, 4500);
  }

  goToSlide(index: number): void {
    this.currentSlide = index;
    clearInterval(this.slideTimer);
    this.startHeroSlider();
  }

  goToTestimonial(index: number): void {
    this.currentTestimonial = index;
  }

  private loadFeaturedPlants(): void {
    this.plantService.getPlants({
      page: 1,
      pageSize: 4,
      priceRangeIds: [],
      treeStyleIds: [],
      potShapeIds: [],
      potSizeIds: [],
      sortBy: 'newest'
    }).subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.featuredPlants = res.data.data;
        }
      }
    });
  }

  onSubscribe(): void {
    if (this.subscribeEmail.trim()) {
      this.subscribeEmail = '';
    }
  }
}
