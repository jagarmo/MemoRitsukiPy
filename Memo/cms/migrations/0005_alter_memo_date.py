# Generated by Django 5.1.2 on 2024-11-03 03:18

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('cms', '0004_alter_memo_date'),
    ]

    operations = [
        migrations.AlterField(
            model_name='memo',
            name='date',
            field=models.DateTimeField(auto_now=True, verbose_name='更新日時'),
        ),
    ]
