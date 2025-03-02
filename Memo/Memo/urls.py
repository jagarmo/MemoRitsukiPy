

"""
URL configuration for Memo project.

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/5.1/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
# from django.contrib import admin
# #from django.urls import path
# from django.urls import path,include
# from rest_framework.routers import DefaultRouter
# from cms.views import UserViewSet,MemoViewSet
# from cms import views
# #from cms.urls import router as cms_router

# router = DefaultRouter()

# router.register(r'users',UserViewSet)
# router.register(r'memos',MemoViewSet)

# urlpatterns = [
#     path('admin/', admin.site.urls),

#     #path('api/',include(router.urls)),

#     path('api/',views.MemosView.as_view()),

# ]

from django.contrib import admin
from django.urls import path, include

urlpatterns = [
    path('admin/', admin.site.urls),
    path('cms/', include('cms.urls')),
]

