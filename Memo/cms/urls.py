# coding: utf-8


# from rest_framework import routers
# from .views import UserViewSet, MemoViewSet


# router = routers.DefaultRouter()
# router.register(r'users', UserViewSet)
# router.register(r'memos', MemoViewSet)


from django.urls import path
from . import views

urlpatterns = [
    path('endpoint/', views.game_data, name='game_data'),
]