import 'package:dio/dio.dart';

class DioConfig {
  static Dio builderConfig() {
    var options = BaseOptions(
      baseUrl: "http://localhost:44338/api/",
      connectTimeout: 15000,
      receiveTimeout: 15000,
      headers: {
        "Content-Type": "application/json",
        // "Authorization":
        //     "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIiLCJuYW1lIjoibWF0ZXVzIiwiZW1haWwiOiJtYXRldXNnYXJjaWEyMDAxQGdtYWlsLmNvbSIsInByb2ZpbGVpbWFnZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hcU5OZzVlTGJpem5WQ2NTaFdKVlZGYTZrdTE1aGdKWmlCUVUxNU40WXFPb3hqVXV2MnpYd2JscDk3Q044V2Y5ZHdLN1pCcEl0MVF3RGpFQkxFYlMwZlVRRXdZQnpFT0VZTkgzbFA3YVI5Y01LOFdfdXhqLUc3SW90TTh3QTVlNXJvSzk2WGExdVpFVlNhYVYzV1lacm5kaUd1UURQMk5zQWZabTcyd1NneGdrTTJjaGFoOWxsbldaTnJhUzNVUEl2dEVhVHpaRi1DUGRyNlo5RHdDalZtNlktaEFSS1QxT1lXZ0gtQTJUV1Z5MmIyaWIyaTFneUV4YzRieGNwMlB2a2FfRV9SdlFJQWNHN2NZRkNCczRMcW9VdHVvYlBGRE5pSk54RmtJSk9Kc2JFNllnajI4dHJOZ0JPNXoxSzJJZGFrc0xHdXhGQW1RbFJyWG1Cb29vS2xmNU1zdmhybWxBMElzUnNYM3U0MEFOX29pSjRtU083UkE5YUNfVFF4cjJKVmVuTVlqbUJ5UVZVOXdxdUxjWDk3MkYwRTVNNmhKcnpQbjVaSFNIVGVtY3lOdGtYRUotR2dKYkZiQ04wZlo1ak1la3hNSkxsbDZZOTdiaHljU040WFVCblpVVnlpSl9fOXpINzNuVHpnRzl3UTZ3aVgxbnF1MlQ2dHZReWpvLTZCWnJnaVRuaHg1TC1za2JFVFQ0NmRldFBRWWsxMFN2b085Z0pMVnNSbGp4TjlpX19mS3JJeXJxZGp0RGVjZ2dSNGR1UVM4NjFhQ29rYW1kRHBvUDE4U1B6ZUJrV2lJN0NuTmlNeXJYZTVpRzhNZWx1bW0zXzlnMzFYdFAxZnh4UG4wcjV0dWxYdy1GM3J4NVJBbE5Jb0pfYlpybWlNcDRBOEFTakgzTlV3VFpQMUw4MzFkTzZKVUZzQWNGcUczOGlvTkV4dENDSWZaRE9SVlR6b19taTVzYj1zMjIwLW5vP2F1dGh1c2VyPTAiLCJwcm9maWxldHlwZWlkIjoiMSIsInByb2ZpbGVUeXBlRGVzY3JpcHRpb24iOiJBZG1pbmlzdHJhZG9yIiwidXNlcm5hbWUiOiJtYXRldXMiLCJuYmYiOjE2NDk4MTI0NTIsImV4cCI6MTY0OTgxOTY1MiwiaWF0IjoxNjQ5ODEyNDUyfQ.YdnXF0teYt2lmNg0u-XujH_34T-vO7hPCqv5DO2dZAQ"
      },
    );

    Dio dio = Dio(options);
    return dio;
  }
}
