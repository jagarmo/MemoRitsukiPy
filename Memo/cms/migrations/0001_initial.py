# Generated by Django 5.1.2 on 2024-11-03 02:48

import django.db.models.deletion
from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Memo',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=255, verbose_name='タイトル')),
                ('date', models.DateTimeField()),
            ],
        ),
        migrations.CreateModel(
            name='Sentence',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('content', models.TextField(blank=True, verbose_name='内容')),
                ('memo', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='sentence', to='cms.memo', verbose_name='メモ')),
            ],
        ),
    ]
