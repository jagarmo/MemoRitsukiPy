# coding: utf-8


from rest_framework import serializers

from .models import User, Memo


class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = ('username', 'password')


class MemoSerializer(serializers.ModelSerializer):
    class Meta:
        model = Memo
        fields = ('name', 'date', 'author')