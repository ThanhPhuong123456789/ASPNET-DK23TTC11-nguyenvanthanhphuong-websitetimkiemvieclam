using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CommonDao
    {
        public class City
        {
            public int Id { get; set; }
            public string Value{ get; set; }
            public string Name { get; set; }
        }

        public class CityDao
        {
            public List<City> GetAllCities()
            {
                City[] cities = new City[]
                {
                    new City() {Id = 1, Value =  " ", Name = "Tất cả" },
                    new City() {Id = 2, Value =  "Hà Nội", Name = "Hà Nội" },
                    new City() {Id = 3, Value =  "Hồ Chí Minh", Name = "Hồ Chí Minh" },
                    new City() {Id = 4, Value =  "Đà Nẵng", Name = "Đà Nẵng" },
                    new City() {Id = 5, Value =  "Hải Phòng", Name = "Hải Phòng" },
                    new City() {Id = 6, Value =  "Cần Thơ", Name = "Cần Thơ" },
                    new City() {Id = 7, Value =  "An Giang", Name = "An Giang" },
                    new City() {Id = 8, Value =  "Bà Rịa - Vũng Tàu", Name = "Bà Rịa - Vũng Tàu" },
                    new City() {Id = 9, Value =  "Bạc Liêu", Name = "Bạc Liêu" },
                    new City() {Id = 10, Value =  "Bắc Kạn", Name = "Bắc Kạn" },
                    new City() {Id = 11, Value =  "Bắc Giang", Name = "Bắc Giang" },
                    new City() {Id = 12, Value =  "Bắc Ninh", Name = "Bắc Ninh" },
                    new City() {Id = 13, Value =  "Bến Tre", Name = "Bến Tre" },
                    new City() {Id = 14, Value =  "Bình Dương", Name = "Bình Dương" },
                    new City() {Id = 15, Value =  "Bình Định", Name = "Bình Định" },
                    new City() {Id = 16, Value =  "Bình Phước", Name = "Bình Phước" },
                    new City() {Id = 17, Value =  "Bình Thuận", Name = "Bình Thuận" },
                    new City() {Id = 18, Value =  "Cà Mau", Name = "Cà Mau" },
                    new City() {Id = 19, Value =  "Cao Bằng", Name = "Cao Bằng" },
                    new City() {Id = 20, Value =  "Đắk Lắk", Name = "Đắk Lắk" },
                    new City() {Id = 21, Value =  "Đắk Nông", Name = "Đắk Nông" },
                    new City() {Id = 22, Value =  "Điện Biên", Name = "Điện Biên" },
                    new City() {Id = 23, Value =  "Đồng Nai", Name = "Đồng Nai" },
                    new City() {Id = 24, Value =  "Đồng Tháp", Name = "Đồng Tháp" },
                    new City() {Id = 25, Value =  "Gia Lai", Name = "Gia Lai" },
                    new City() {Id = 26, Value =  "Hà Giang", Name = "Hà Giang" },
                    new City() {Id = 27, Value =  "Hà Nam", Name = "Hà Nam" },
                    new City() {Id = 28, Value =  "Hà Tĩnh", Name = "Hà Tĩnh" },
                    new City() {Id = 29, Value =  "Hải Dương", Name = "Hải Dương" },
                    new City() {Id = 30, Value =  "Hậu Giang", Name = "Hậu Giang" },
                    new City() {Id = 31, Value =  "Hòa Bình", Name = "Hòa Bình" },
                    new City() {Id = 32, Value =  "Hưng Yên", Name = "Hưng Yên" },
                    new City() {Id = 33, Value =  "Khánh Hòa", Name = "Khánh Hòa" },
                    new City() {Id = 34, Value =  "Kiên Giang", Name = "Kiên Giang" },
                    new City() {Id = 35, Value =  "Kon Tum", Name = "Kon Tum" },
                    new City() {Id = 36, Value =  "Lai Châu", Name = "Lai Châu" },
                    new City() {Id = 37, Value =  "Lâm Đồng", Name = "Lâm Đồng" },
                    new City() {Id = 38, Value =  "Lạng Sơn", Name = "Lạng Sơn" },
                    new City() {Id = 39, Value =  "Lào Cai", Name = "Lào Cai" },
                    new City() {Id = 40, Value =  "Long An", Name = "Long An" },
                    new City() {Id = 41, Value =  "Nam Định", Name = "Nam Định" },
                    new City() {Id = 42, Value =  "Nghệ An", Name = "Nghệ An" },
                    new City() {Id = 43, Value =  "Ninh Bình", Name = "Ninh Bình" },
                    new City() {Id = 44, Value =  "Ninh Thuận", Name = "Ninh Thuận" },
                    new City() {Id = 45, Value =  "Phú Thọ", Name = "Phú Thọ" },
                    new City() {Id = 46, Value =  "Phú Yên", Name = "Phú Yên" },
                    new City() {Id = 47, Value =  "Quảng Bình", Name = "Quảng Bình" },
                    new City() {Id = 48, Value =  "Quảng Nam", Name = "Quảng Nam" },
                    new City() {Id = 49, Value =  "Quảng Ngãi", Name = "Quảng Ngãi" },
                    new City() {Id = 50, Value =  "Quảng Ninh", Name = "Quảng Ninh" },
                    new City() {Id = 51, Value =  "Quảng Trị", Name = "Quảng Trị" },
                    new City() {Id = 52, Value =  "Sóc Trăng", Name = "Sóc Trăng" },
                    new City() {Id = 53, Value =  "Sơn La", Name = "Sơn La" },
                    new City() {Id = 54, Value =  "Tây Ninh", Name = "Tây Ninh" },
                    new City() {Id = 55, Value =  "Thái Bình", Name = "Thái Bình" },
                    new City() {Id = 56, Value =  "Thái Nguyên", Name = "Thái Nguyên" },
                    new City() {Id = 57, Value =  "Thanh Hóa", Name = "Thanh Hóa" },
                    new City() {Id = 58, Value =  "Thừa Thiên Huế", Name = "Thừa Thiên Huế" },
                    new City() {Id = 59, Value =  "Tiền Giang", Name = "Tiền Giang" },
                    new City() {Id = 60, Value =  "Trà Vinh", Name = "Trà Vinh" },
                    new City() {Id = 61, Value =  "Tuyên Quang", Name = "Tuyên Quang" },
                    new City() {Id = 62, Value =  "Vĩnh Long", Name = "Vĩnh Long" },
                    new City() {Id = 63, Value =  "Vĩnh Phúc", Name = "Vĩnh Phúc" },
                };

                return cities.OrderBy(c => c.Id).ToList();
            }
        }

    }
}
